using Afrimart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Afrimart.Dto;
using Afrimart.Dto.Public;
using Afrimart.Services;
using Afrimart.ViewModels.Home;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using ServiceHelper.Requests;

namespace Afrimart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Services.IAuthenticationService _authenticationService;
        private readonly IRequestManager _requestManager;

        public HomeController(ILogger<HomeController> logger, Services.IAuthenticationService authenticationService, IRequestManager requestManager)
        {
            _logger = logger;
            _authenticationService = authenticationService;
            _requestManager = requestManager;
        }

        public async Task<IActionResult> Index()
        {
            // Index view model
            // get trending products -> Fetch top 8 products order by sales count desc
            // imgUrl, prodName, categName, catId, price, rating, 
            // for quick view: galleryImgUrls, IsOnSale, salesPrice, prodCount, description
            // 3 most popular categories to showcase (logic for this??
            // categoryImg, name, 
            // 1 special category: (maybe Ankara for now) logic??
            // pull top 12 products in the category

            var apiResponse = await _requestManager.Send<string, BaseApiResponseDto<HomepageDataResponseDto>>("/api/Home/default", null,
                HttpMethod.Get);

            var model = new HomepageViewModel()
            {
                SpecialCategory = apiResponse.Data.SpecialCategory,
                PopularCategories = apiResponse.Data.PopularCategories,
                SpecialCategoryProducts = apiResponse.Data.SpecialCategoryProducts,
                TrendingProducts = apiResponse.Data.TrendingProducts
            };
            return View(model);
        }

        public async Task<IActionResult> GetQuickViewPartialView(string psin)
        {
            var apiResponse = await _requestManager.Send<string, BaseApiResponseDto<HomeProductCard>>($"/api/Home/product/{psin}", null,
                HttpMethod.Get);
            
            return PartialView("_ProductQuickView", apiResponse.Data);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        //[Authorize(Policy = "admin")]
        public IActionResult Test()
        {
            return View();
        }

        [Authorize(Roles = "Seller")]
        //[Authorize(Policy = "admin")]
        public IActionResult Seller()
        {
            return RedirectToAction("Test");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<UserModel> AuthenticateUser(string username, string password)
        {
            // For demonstration purposes, authenticate a user
            // with a static email address. Ignore the password.
            // Assume that checking the database takes 500ms

            await Task.Delay(500);

            if (username == "sdk@test.com")
            {
                return new UserModel()
                {
                    UserName = "sdk@test.com",
                    Password = password,
                    Email = username,
                    FullName = "Maria Rodriguez"
                };
            }
            else
            {
                return null;
            }
        }
    } 
}
