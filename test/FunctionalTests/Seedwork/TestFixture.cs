using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Respawn;
using System;
using System.Threading.Tasks;
using WeCode.Api.Data;

namespace FunctionalTests.Seedwork
{
    public class TestFixture
    {
        private IHost host;
        private static Checkpoint checkpoint = new Checkpoint
        {
            TablesToIgnore = new[] { "__EFMigrationsHistory" },
            WithReseed = true
        };

        public TestServer TestServer { get; private set; }

        public TestFixture()
        {
            host = CreateHost();
            host.Start();
            host.MigrateDbContext<NotesContext>();
            TestServer = host.GetTestServer();
        }

        private IHost CreateHost()
        {
            return new HostBuilder()
                .ConfigureWebHost(configure =>
                {
                    configure
                        .ConfigureServices(services =>
                            services.AddSingleton<IServer>(sp =>
                                new TestServer(sp)))
                        .UseStartup<TestStartup>();
                })
                .ConfigureAppConfiguration(configure =>
                {
                    configure.AddJsonFile("appsettings.json", optional: false);
                })
                .Build();
        }

        public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> func)
        {
            using (var scope = host.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                await func(scope.ServiceProvider);
            }
        }

        public async Task ExecuteDbContextAsync(Func<NotesContext, Task> func)
        {
            await ExecuteScopeAsync(sp => func(sp.GetService<NotesContext>()));
        }

        internal static void ResetDatabase()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
            checkpoint.Reset(configuration.GetConnectionString(Constants.SqlServer)).Wait();
        }
    }
}
