using Afrimart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Afrimart.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Afrimart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Services.IAuthenticationService _authenticationService;

        public HomeController(ILogger<HomeController> logger, Services.IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            return View();
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
