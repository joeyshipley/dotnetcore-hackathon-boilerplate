using System.Threading.Tasks;
using BOS.Webclient.Infrastructure.Services;
using BOS.Webclient.Models.Accounts;
using Microsoft.AspNetCore.Identity;

namespace BOS.Webclient.Services.Accounts
{
    public interface IAccountLogoutInteractor
        : IInteractor
    {}

    public class AccountLogoutInteractor : InteractorBase, IAccountLogoutInteractor
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountLogoutInteractor(
            SignInManager<ApplicationUser> signInManager
        )
        {
            _signInManager = signInManager;
        }

        public async Task<VoidInteractorResult> Call()
        {
            return await PerformCall(async () => 
            {
                await _signInManager.SignOutAsync();
                return VoidInteractorResult.ForSuccess();
            });
        }
    }
}