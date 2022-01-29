using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Afrimart.Common;
using Afrimart.Dto;
using Afrimart.Services;
using Afrimart.ViewModels.Sellers;
using Microsoft.AspNetCore.Authorization;
using ServiceHelper.Requests;
using IAuthorizationService = Microsoft.AspNetCore.Authorization.IAuthorizationService;

namespace Afrimart.Controllers
{
    [Authorize]
    public class SellersController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAfrimartAuthorizationService _authorizationService;
        private readonly IRequestManager _requestManager;

        public SellersController(IAuthenticationService authenticationService, IAfrimartAuthorizationService authorizationService, IRequestManager requestManager)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _requestManager = requestManager;
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
            var model = new SetupSellerProfileViewModel()
            {
                EmailAddress = _authorizationService.GetUserEmail()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Onboarding(SetupSellerProfileViewModel model)
        {
            if (ModelState.IsValid)
            { 
                var result = await _requestManager.Send<SetupSellerProfileViewModel, BaseApiResponseDto<string>>("/api/Sellers/store", model,
                    HttpMethod.Post);

                if (result.Success)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Request failed!");
            }
            return View(model);
        }
    }
}
