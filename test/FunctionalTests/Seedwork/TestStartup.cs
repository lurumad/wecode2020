using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeCode.Api;
using WeCode.Api.Data;

namespace FunctionalTests.Seedwork
{
    public class TestStartup
    {
        private readonly IConfiguration configuration;

        public TestStartup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ApiConfiguration.ConfigureServices(services)
                .AddDbContext<NotesContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString(Constants.SqlServer), sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(TestStartup).Assembly.FullName);
                    });
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            ApiConfiguration.Configure(
                app,
                preConfigure: _ => _,
                postConfigure: _ => _);
        }
    }
}
