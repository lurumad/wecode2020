using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using WeCode.Api.Models;

namespace WeCode.Api
{
    public static class ApiConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services) =>
            services
                .AddCustomApiVersioning()
                .AddCustomProblemDetails()
                .AddVersionedApiExplorer()
                .AddMvc()
                .AddFluentValidation(setup => setup.RegisterValidatorsFromAssemblyContaining<NoteValidator>())
                .AddApplicationPart(typeof(ApiConfiguration).Assembly)
                .AddNewtonsoftJson()
                .Services;

        public static IApplicationBuilder Configure(
            IApplicationBuilder app, 
            Func<IApplicationBuilder, IApplicationBuilder> preConfigure,
            Func<IApplicationBuilder, IApplicationBuilder> postConfigure)
        {
            var applicationBuilder = preConfigure(app)
                .UseProblemDetails()
                .UseRouting();

            return postConfigure(applicationBuilder)
                .UseEndpoints(configure => configure.MapDefaultControllerRoute());
        }
    }
}
