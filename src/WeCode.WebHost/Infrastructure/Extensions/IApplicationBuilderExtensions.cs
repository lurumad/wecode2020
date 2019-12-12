using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app, IApiVersionDescriptionProvider apiVersion)
        {
            return app
                .UseSwagger()
                .UseSwaggerUI(setup =>
                {
                    foreach (var description in apiVersion.ApiVersionDescriptions)
                    {
                        setup.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
        }
    }
}
