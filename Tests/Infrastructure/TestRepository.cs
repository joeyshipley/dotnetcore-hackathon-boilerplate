using BOS.Webclient.Infrastructure.Db.ContextControl;
using BOS.Webclient.Infrastructure.Db.Repositories;
using BOS.Webclient.Models.Accounts;

namespace BOS.Tests.Infrastructure
{
    public class TestRepository : RepositoryBase
    {
        public TestRepository(IContextSessionProvider contextSessionProvider) 
            : base(contextSessionProvider) {}
        
        public void ClearAllData()
        {
            ClearApplicationUsers();
        }

        public void ClearApplicationUsers()
        {
            RemoveEntityAll<ApplicationUser>();
        }

        public T Create<T>(T entity)
            where T : class
        {
            return AddEntity(entity);
        }
    }
}