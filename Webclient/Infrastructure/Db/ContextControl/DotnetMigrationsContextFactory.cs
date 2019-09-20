using System.IO;
using BOS.Webclient.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BOS.Webclient.Infrastructure.Db.ContextControl
{
    public class DotnetMigrationsContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private const int COMMAND_TIMEOUT_SECONDS = 60;
        // TODO: consider better placement of these
        private const string APP_SETTINGS_DB_KEY = "DefaultConnection";
        private const string APP_SETTINGS_LOCATION = "../Webclient/";
        private const string APP_SETTINGS_FILENAME = "appsettings.json";

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var dir = Directory.GetCurrentDirectory();
            var path = Path.Combine(dir, APP_SETTINGS_LOCATION);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile(APP_SETTINGS_FILENAME)
                .Build();

            var connectionString = configuration.GetConnectionString(APP_SETTINGS_DB_KEY);
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(COMMAND_TIMEOUT_SECONDS))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            var options = builder.Options;

            return new ApplicationDbContext(options);
        }
    }
}