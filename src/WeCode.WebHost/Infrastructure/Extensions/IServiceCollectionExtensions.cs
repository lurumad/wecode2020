using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using WeCode.Api.Data;
using WeCode.WebHost;
using WeCode.WebHost.Infrastructure.OpenApi;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            return services
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>()
                .AddSwaggerGen();
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDbContext<NotesContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString(Constants.SqlServer), sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).Assembly.FullName);
                    });
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
