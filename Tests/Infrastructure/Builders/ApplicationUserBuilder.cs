using BOS.Webclient.Models.Accounts;

namespace BOS.Tests.Infrastructure.Builders
{
    public static class ApplicationUserBuilder
    {
        public static ApplicationUser AsValid(string email)
        {
            return new ApplicationUser
            {
                Email = email,
                Name = "Ron Weasley"
            };
        }
    }
}