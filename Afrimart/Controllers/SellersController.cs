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

        public async Task<IActionResult> Products()
        { 
            var apiResponse = await _requestManager.Send<string, BaseApiResponseDto<ProductListPageDataDto>>("/api/Sellers/prod-list-data", null,
                HttpMethod.Get);
            var data = apiResponse.Data;

            var model = new SellerProductListViewModel()
            {
                DashboardHeaderViewModel = new DashboardHeaderViewModel()
                {
                    StoreName = data.DashboardData.StoreName,
                    TotalSales = data.DashboardData.TotalSales,
                    RatingCount = data.DashboardData.RatingCount,
                    MemberSince = data.DashboardData.MemberSince,
                    AverageRating = data.DashboardData.AverageRating,
                    LogoUri = data.DashboardData.LogoUri
                },
                Products = data.Products.Select(x => new SellerProductCardViewModel()
                {
                    ImageUri = x.ImageUri,
                    Name = x.Name,
                    Price =  x.Price,
                    TotalEarnings = x.TotalEarnings,
                    TotalSales = x.TotalSales
                }).ToList()
            };


            return View(model);
        }
        public async Task<IActionResult> AddProduct()
        {  
            var apiResponse = await _requestManager.Send<string, BaseApiResponseDto<AddProductPageDataDto>>("/api/Sellers/add-prod-data", null,
                HttpMethod.Get);
            var data = apiResponse.Data;

            var model = new AddProductViewModel()
            {
                DashboardHeaderViewModel = new DashboardHeaderViewModel()
                {
                    StoreName = data.DashboardData.StoreName,
                    TotalSales = data.DashboardData.TotalSales,
                    RatingCount = data.DashboardData.RatingCount,
                    MemberSince = data.DashboardData.MemberSince,
                    AverageRating = data.DashboardData.AverageRating,
                    LogoUri = data.DashboardData.LogoUri
                },
                ProductCategories = data.Categories.Select(x => new SelectListItem()
                {
                    Value = x.Value,
                    Text = x.Text
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel model)
        {
            if (ModelState.IsValid)
            { 
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

                    var uploadResult = await _requestManager.Send<List<ProductFileUploadDto>, BaseApiResponseDto<string>>($"/api/Sellers/products/{productId}/files", imagesToUpload,
                        HttpMethod.Post);
                    if (uploadResult.Success)
                    {
                        return RedirectToAction("Products");
                    }
                    
                    ModelState.AddModelError("apiError", uploadResult.Message);
                }
                ModelState.AddModelError("apiError", result.Message);
            }

            // Load data again from API
            var apiResponse = await _requestManager.Send<string, BaseApiResponseDto<AddProductPageDataDto>>("/api/Sellers/add-prod-data", null,
                HttpMethod.Get);
            var data = apiResponse.Data;
            model.DashboardHeaderViewModel = new DashboardHeaderViewModel()
            {
                StoreName = data.DashboardData.StoreName,
                TotalSales = data.DashboardData.TotalSales,
                RatingCount = data.DashboardData.RatingCount,
                MemberSince = data.DashboardData.MemberSince,
                AverageRating = data.DashboardData.AverageRating,
                LogoUri = data.DashboardData.LogoUri
            };
            model.ProductCategories = data.Categories.Select(x => new SelectListItem()
            {
                Value = x.Value,
                Text = x.Text
            }).ToList();
             

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
            Random rnd = new Random();
            return $"https://source.unsplash.com/random/250x150?sig={rnd.Next()}"; //"https://picsum.photos/250/150";
        }
    } 
}
