using System;
using System.Collections.Generic;
using BOS.Webclient.Infrastructure.IoC;
using BOS.Tests.Infrastructure.Builders;
using BOS.Tests.Infrastructure.Fakes;
using BOS.Webclient.Infrastructure.Db.ContextControl;
using BOS.Webclient.Infrastructure.Services;
using BOS.Webclient.Models.Accounts;
using Microsoft.Extensions.DependencyInjection;

namespace BOS.Tests.Infrastructure.TestBases
{
    public class IntegratedFor<T> : ArrangeActAssertOn
        where T : class
    {
        protected readonly List<Action<IServiceCollection>> DependencyFakes = new List<Action<IServiceCollection>>();

        protected T SUT;

        public IntegratedFor()
        {
            SharedBeforeAll();
            SUT = Resolve<T>();
        }

        protected TResolveFor Resolve<TResolveFor>()
        {
            return DependencyRegistrations.Resolve<TResolveFor>(DependencyFakes);
        }

        private void SharedBeforeAll()
        {
            DependencyFakes.Add((services) => 
            {
                services.AddScoped<IDbContextFactory, EntityFrameworkInMemoryDbContextFactory>();
            });
        }

        protected (IUnitOfWork UnitOfWork, TestRepository Repository) DbHelper()
        {
            var unitOfWork = Resolve<IUnitOfWork>();
            var sessionProvider = Resolve<IContextSessionProvider>();
            var repository = new TestRepository(sessionProvider);
            return (UnitOfWork: unitOfWork, Repository: repository);
        }

        protected ApplicationUser CreateApplicationUser(string email = null)
        {
            ApplicationUser user = null;
            var unitOfWork = Resolve<IUnitOfWork>();
            var sessionProvider = Resolve<IContextSessionProvider>();
            unitOfWork.Worker(() =>
            {
                var repository = new TestRepository(sessionProvider);
                email = !string.IsNullOrEmpty(email) 
                    ? email
                    : $"test-{ Guid.NewGuid() }@user.com";
                user = repository.Create(ApplicationUserBuilder.AsValid(email));
                unitOfWork.SaveChanges();
            });
            return user;
        }

        protected void CleanupDb()
        {
            var unitOfWork = Resolve<IUnitOfWork>();
            var sessionProvider = Resolve<IContextSessionProvider>();
            unitOfWork.Worker(() =>
            {
                var repository = new TestRepository(sessionProvider);
                repository.ClearAllData();
                unitOfWork.SaveChanges();
            });
        }
    }
}