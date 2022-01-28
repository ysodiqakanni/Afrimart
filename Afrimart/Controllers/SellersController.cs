using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Afrimart.Services;
using Afrimart.ViewModels.Sellers;
using Microsoft.AspNetCore.Authorization;
using IAuthorizationService = Afrimart.Services.IAuthorizationService;

namespace Afrimart.Controllers
{
    [Authorize]
    public class SellersController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthorizationService _authorizationService;

        public SellersController(IAuthenticationService authenticationService, IAuthorizationService authorizationService)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {

            return View(new RegisterSellerViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterSellerViewModel model)
        {
            if (ModelState.IsValid)
            { 
                // Todo: Send a confirmation email
                var result = await _authenticationService.Register(model.Email, model.Password);
                if (result.Success)
                {
                    await _authorizationService.WriteCustomClaimsAsync(new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, result.Data.User.Email)
                    });
                    return RedirectToAction("Onboarding");
                }
               ModelState.AddModelError("", result.Message);
            }
           
            return View();
        }

        public async Task<IActionResult> Onboarding()
        {
            return View();
        }
    }
}
