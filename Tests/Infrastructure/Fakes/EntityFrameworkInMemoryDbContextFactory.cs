using BOS.Webclient.Db;
using BOS.Webclient.Infrastructure.Db.ContextControl;
using Microsoft.EntityFrameworkCore;

namespace BOS.Tests.Infrastructure.Fakes
{
    public class EntityFrameworkInMemoryDbContextFactory : IDbContextFactory
    {
        private const string DB_NAME = "EF_INMEMORY_TEST_DB";

        public ApplicationDbContext For()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(DB_NAME);
            var options = builder.Options;

            return new ApplicationDbContext(options);
        }
    }
}