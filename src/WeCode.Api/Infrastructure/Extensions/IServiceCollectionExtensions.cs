using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeCode.Api;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services)
        {
            return services
                .AddProblemDetails()
                .Configure<ApiBehaviorOptions>(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Instance = context.HttpContext.Request.Path,
                            Status = StatusCodes.Status400BadRequest,
                            Type = $"https://httpstatuses.com/400",
                            Detail = Constants.ModelStateValidation
                        };

                        return new BadRequestObjectResult(problemDetails)
                        {
                            ContentTypes =
                            {
                                Constants.ContentTypes.ProblemJson,
                                Constants.ContentTypes.ProblemXml
                            }
                        };
                    };
                });
        }

        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
        {
            return services
                .AddApiVersioning(version =>
                {
                    version.DefaultApiVersion = new ApiVersion(1, 0);
                    version.ReportApiVersions = true;
                    version.AssumeDefaultVersionWhenUnspecified = true;
                    version.UseApiBehavior = true;
                });
        }
    }
}
