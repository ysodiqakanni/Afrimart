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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHelper.Requests;
using IAuthorizationService = Microsoft.AspNetCore.Authorization.IAuthorizationService;
using Afrimart.Dto.Stores;

namespace Afrimart.Controllers
{
    [Authorize]
    public class SellersController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAfrimartAuthorizationService _authorizationService;
        private readonly IRequestManager _requestManager;
        private DashboardHeaderViewModel dashboardTestData;
        private List<SelectListItem> testCategories;

        public SellersController(IAuthenticationService authenticationService, IAfrimartAuthorizationService authorizationService, IRequestManager requestManager)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _requestManager = requestManager;

            dashboardTestData = new DashboardHeaderViewModel()
            {
                AverageRating = 4.6,
                LogoUri = "https://source.unsplash.com/random",
                MemberSince = "July 2018",
                RatingCount = 235,
                StoreName = "Molly Kitchen",
                TotalSales = 600
            };
            testCategories = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = "1",
                    Text = "Seasonings"
                },
                new SelectListItem()
                {
                    Value = "2",
                    Text = "Seasonings"
                },
                new SelectListItem()
                {
                    Value = "3",
                    Text = "Fish"
                },
                new SelectListItem()
                {
                    Value = "4",
                    Text = "Poultry"
                },
                new SelectListItem()
                {
                    Value = "5",
                    Text = "Cookies"
                },
                new SelectListItem()
                {
                    Value = "6",
                    Text = "Snacks"
                },
            };
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
            ModelState.AddModelError("", "An error occurred");
            return View(model);
        }

        public IActionResult Products()
        {
            var model = new SellerProductListViewModel()
            {
                DashboardHeaderViewModel = dashboardTestData,
                Products = new List<SellerProductCardViewModel>()
                {
                    new SellerProductCardViewModel()
                    {
                        ImageUri = "https://picsum.photos/250/150",
                        Name = "This is a test product",
                        Price = 12.99m,
                        TotalEarnings = 1204.95m,
                        TotalSales = 98
                    }
                }
            };
            return View(model);
        }
        public IActionResult AddProduct()
        {
            // Todo: pull all categories
            // Todo: pull dashboard data
             

            var model = new AddProductViewModel()
            {
                DashboardHeaderViewModel = dashboardTestData,
                ProductCategories = testCategories
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                // first save the product to the db
                // upload the images to file server
                // save the displayUrl againt product entity
                // create new ProfuctFiles in the db

                var result = await _requestManager.Send<AddProductViewModel, BaseApiResponseDto<string>>("/api/Sellers/products", model,
                    HttpMethod.Post);
                if (result.Success)
                {
                    string productId = result.Data;
                    var imagesToUpload = new List<ProductFileUploadDto>();
                    string displayImgUrl = UploadFile(model.DisplayImage, $"/products/{productId}");
                    imagesToUpload.Add(new ProductFileUploadDto()
                    {
                        FileType = FileType.DisplayImage,
                        FileName = $"{Guid.NewGuid()}{model.DisplayImage.FileName.Substring(0, 8)}",
                        FileUri = displayImgUrl
                    });

                    foreach (var file in model.GalleryImages)
                    {
                        string url = UploadFile(model.DisplayImage, $"/products/{productId}");
                        imagesToUpload.Add(new ProductFileUploadDto()
                        {
                            FileType = FileType.GalleryImages,
                            FileName = $"{Guid.NewGuid()}{file.FileName.Substring(0, 8)}",
                            FileUri = url
                        });
                    }
                    // save images
                    // save the display image

                    // if there are gallery images, save them
                    return RedirectToAction("Products");
                }
                ModelState.AddModelError("apiError", result.Message);
            }

            model.DashboardHeaderViewModel = dashboardTestData; 
            model.ProductCategories = testCategories;

            ModelState.AddModelError("", "Unable to save product");
            return View(model);
        }
        public IActionResult Sales()
        {
            return View();
        }
        public IActionResult Payouts()
        {
            return View();
        }

        private string UploadFile(IFormFile file, string folder)
        {
            // Todo: Implement file upload in a file service file
            return "https://picsum.photos/250/150";
        }
    } 
}
