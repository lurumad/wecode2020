using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeCode.Api;
using WeCode.Api.Data;

namespace WeCode.WebHost
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ApiConfiguration
                .ConfigureServices(services)
                .AddOpenApi()
                .AddDbContext(configuration);
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider apiVersion)
        {
            ApiConfiguration.Configure(
                app,
                preConfigure: pre => pre
                    .AddIf(env.IsDevelopment(), x => x.UseDeveloperExceptionPage())
                    .UseOpenApi(apiVersion),
                postConfigure: post => post);
        }
    }
}
