using System.Threading.Tasks;
using BOS.Webclient.Infrastructure.Services;
using BOS.Webclient.Models.Accounts;
using BOS.Webclient.Models.Messaging;
using Microsoft.AspNetCore.Identity;

namespace BOS.Webclient.Services.Accounts
{
    public interface IAccountLoginInteractor
        : IInteractor<LoginRequest> {}

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }


    public class AccountLoginInteractor : InteractorBase, IAccountLoginInteractor
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountLoginInteractor(
            SignInManager<ApplicationUser> signInManager
        )
        {
            _signInManager = signInManager;
        }

        public async Task<VoidInteractorResult> Call(LoginRequest request)
        {
            return await PerformCall(async () => 
            {
                var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, true);
                
                if (!result.Succeeded)
                    return  VoidInteractorResult.ForFailure(Message.For("InvalidLogin", "Log in failed. Please check info and try again."));
                if (result.IsLockedOut)
                    return  VoidInteractorResult.ForFailure(Message.For("AccountLockedOut", "Login attempts exceeded maximum. Account locked."));

                return VoidInteractorResult.ForSuccess();
            });
        }
    }
}