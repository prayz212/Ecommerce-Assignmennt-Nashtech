using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CustomerSite.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Shared.Clients;

namespace Namespace
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

            if (!string.IsNullOrEmpty(returnUrl)) ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login, string returnUrl = "")
        {
            var token = await _accountService.Login(login);
            if (token is null)
            {
                ViewBag.LoginErrorMsg = "Tên tài khoản hoặc mật khẩu không đúng";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, token.Token),
                new Claim(ClaimTypes.Expiration, token.Expiration.ToLocalTime().ToString()),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}