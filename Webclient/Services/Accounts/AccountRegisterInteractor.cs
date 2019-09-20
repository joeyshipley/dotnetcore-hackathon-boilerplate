using System.Linq;
using System.Threading.Tasks;
using BOS.Webclient.Infrastructure.Services;
using BOS.Webclient.Models.Accounts;
using BOS.Webclient.Models.Messaging;
using Microsoft.AspNetCore.Identity;

namespace BOS.Webclient.Services.Accounts
{
    public interface IAccountRegisterInteractor
        : IInteractor<RegisterRequest>
    {}

    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }

    public class AccountRegisterInteractor : InteractorBase, IAccountRegisterInteractor
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRegisterInteractor(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<VoidInteractorResult> Call(RegisterRequest request)
        {
            return await PerformCall(async () => 
            {
                if(request.Password != request.PasswordConfirmation)
                    return VoidInteractorResult.ForFailure(Message.For("PasswordMismatch", "The passwords did not match."));

                var user = new ApplicationUser 
                { 
                    UserName = request.Email, 
                    Email = request.Email, 
                    Name = request.Name 
                };
                var validation = user.Validate();
                if(!validation.IsValid)
                    return VoidInteractorResult.ForFailure(validation.Messages);

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                    return VoidInteractorResult.ForFailure(
                        result.Errors.Select(x => Message.For(x.Code, x.Description)).ToList()
                    );

                await _signInManager.SignInAsync(user, false);

                return VoidInteractorResult.ForSuccess();
            });
        }
    }
}