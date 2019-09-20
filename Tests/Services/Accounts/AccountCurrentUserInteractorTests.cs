using System;
using System.Linq;
using System.Threading.Tasks;
using BOS.Tests.Infrastructure.TestBases;
using BOS.Webclient.Infrastructure.Services;
using BOS.Webclient.Models.Accounts;
using BOS.Webclient.Services.Accounts;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BOS.Tests.Application.Accounts
{
    [TestClass]
    public class AccountCurrentUserInteractorTests
        : IntegratedFor<IAccountCurrentUserInteractor>
    {
        [TestCleanup]
        public void AfterEach()
        {
            CleanupDb();
        }

        [TestMethod]
        public async Task When_All_IsWell()
        {
            ApplicationUser user = null;
            Arrange(() =>
            {
                user = CreateApplicationUser();
            });

            var result = await Act(() => 
            {
                var request = new LoggedInRequest { UserId = user.Id };
                return SUT.Call(request);
            });

            Assert(() => 
            {
                var model = result.Data;
                result.ActionSuccess.Should().BeTrue("ActionSuccess was not correct.");
                model.Email.Should().Be(user.Email, "Email was not correct.");
                model.Name.Should().Be(user.Name, "Name was not correct.");
                model.CreatedOn.Should().NotBe(default(DateTime), "CreatedOn was not correct.");
                model.UpdatedOn.Should().NotBe(default(DateTime), "UpdatedOn was not correct.");
            });
        }
    }
}