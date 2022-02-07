using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Afrimart.Common;
using Afrimart.Dto;
using Afrimart.Services;
using Afrimart.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using IAuthorizationService = Microsoft.AspNetCore.Authorization.IAuthorizationService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Afrimart.Controllers
{
    public class AccountController : Controller
    {
        private readonly Services.IAuthenticationService _authenticationService;
        private readonly IAfrimartAuthorizationService _authorizationService;

        public AccountController(Services.IAuthenticationService authenticationService, IAfrimartAuthorizationService authorizationService)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
        }

        [AllowAnonymous] 
        public async Task<IActionResult> Login(string returnUrl)
        {
            var model = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _authenticationService.Login(model.Email, model.Password);
                if (result.Success)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, result.User.Email),
                        new Claim("Token", result.Token)
                    };
                    if (!string.IsNullOrEmpty(result.User.Role))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, result.User.Role));
                    }
                    await _authorizationService.WriteCustomClaimsAsync(claims);
                    if (!string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return LocalRedirect(model.ReturnUrl);
                    }

                    if (result.User.Role == AfrimartConstants.SELLER_ROLE)
                    {
                        return RedirectToAction("Products", "Sellers");
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Login failed");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

    }
}
