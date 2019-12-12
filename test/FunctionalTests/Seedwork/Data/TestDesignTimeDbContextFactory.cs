using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using WeCode.Api.Data;

namespace FunctionalTests.Seedwork.Data
{
    public class TestDesignTimeDbContextFactory : IDesignTimeDbContextFactory<NotesContext>
    {
        public NotesContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var options = new DbContextOptionsBuilder<NotesContext>()
                .UseSqlServer(configuration.GetConnectionString(nameof(Constants.SqlServer)), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(TestDesignTimeDbContextFactory).Assembly.FullName);
                })
                .Options;

            return new NotesContext(options);
        }
    }
}
