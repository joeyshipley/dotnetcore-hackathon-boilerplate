using System.Threading.Tasks;
using BOS.Webclient.Infrastructure.Controllers;
using BOS.Webclient.Infrastructure.Controllers.Filters;
using BOS.Webclient.Services.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BOS.Webclient.Controllers
{
    [Route("account")]
    public class AccountController : ApplicationControllerBase
    {
        private readonly IAccountLoginInteractor _accountLogin;
        private readonly IAccountLogoutInteractor _accountLogout;
        private readonly IAccountRegisterInteractor _accountRegister;
        private readonly IAccountCurrentUserInteractor _accountCurrentUser;

        public AccountController(
            IAccountLoginInteractor accountLogin,
            IAccountLogoutInteractor accountLogout,
            IAccountRegisterInteractor accountRegister,
            IAccountCurrentUserInteractor accountCurrentUser
        )
        {
            _accountLogin = accountLogin;
            _accountLogout = accountLogout;
            _accountRegister = accountRegister;
            _accountCurrentUser = accountCurrentUser;
        }

        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(CurrentUserFilter))]
        public async Task<IActionResult> Index()
        {
            var request = UserRequest();
            var result = await _accountCurrentUser.Call(request);
            return View(result);
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<JsonResult> Login([FromBody] LoginRequest request)
        {
            var result = await _accountLogin.Call(request);
            return Result(result);
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<JsonResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _accountRegister.Call(request);
            return Result(result);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountLogout.Call();
            return LocalRedirect("/");
        }
    }
}