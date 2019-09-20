using System;
using BOS.Webclient.Infrastructure.Db.ContextControl;
using BOS.Webclient.Infrastructure.Db.Repositories;
using BOS.Webclient.Models.Accounts;

namespace BOS.Webclient.Db.Accounts
{
    public interface IApplicationUserRepository
    {
        ApplicationUser FindComplete(Guid userId);
        ApplicationUser Create(ApplicationUser user);
        ApplicationUser Update(ApplicationUser user);
    }

    public class ApplicationUserRepository : RepositoryBase, IApplicationUserRepository
    {
        public ApplicationUserRepository(IContextSessionProvider contextSessionProvider)
            : base(contextSessionProvider) {}

        public ApplicationUser FindComplete(Guid userId)
        {
            return FindReadonlyBy<ApplicationUser>(x => x.Id == userId);

            // NOTE: example of fetching children entities.

//            return FindReadonlyBy<ApplicationUser>(
//                x => x.Id == userId,
//                includeChildren => includeChildren.ChildCollectionName);
        }
        
        public ApplicationUser Create(ApplicationUser user)
        {
            // NOTE: the creation of app user is currently being handled by 
            // .NET services. When time allows, conver those over to the
            // apps standard flow.

//            user.CreatedOn = DateTime.UtcNow;
//            user.UpdatedOn = DateTime.UtcNow;
            return AddEntity(user);
        }

        public ApplicationUser Update(ApplicationUser user)
        {
            user.UpdatedOn = DateTime.UtcNow;
            return UpdateEntity(user);
        }
    }
}