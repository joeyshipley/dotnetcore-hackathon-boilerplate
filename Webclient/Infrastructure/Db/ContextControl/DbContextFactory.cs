using BOS.Webclient.Db;
using BOS.Webclient.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace BOS.Webclient.Infrastructure.Db.ContextControl
{
    public interface IDbContextFactory
    {
        ApplicationDbContext For();
    }

    public class DbContextFactory : IDbContextFactory
    {
        private const int COMMAND_TIMEOUT_SECONDS = 60;
        private readonly ISettingsProvider _settingsProvider;

        public DbContextFactory(
            ISettingsProvider settingsProvider
        )
        {
            _settingsProvider = settingsProvider;
        }

        public ApplicationDbContext For()
        {
            var connectionString = _settingsProvider.DatabaseConnectionString();
            // TODO: cleanup duplication of this - see DotnetMigrationsContextFactory
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(COMMAND_TIMEOUT_SECONDS))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            var options = builder.Options;

            return new ApplicationDbContext(options);
        }
    }
}