using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Afrimart.Dto;
using Afrimart.Dto.Products;
using Afrimart.Dto.Public;
using Afrimart.ViewModels.Products;
using ServiceHelper.Requests;

namespace Afrimart.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IRequestManager _requestManager;

        public ProductsController(IRequestManager requestManager)
        {
            _requestManager = requestManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("products/{psin}/{name}")]
        public async Task<IActionResult> DisplayProduct(string psin, string name)
        {
            //var model = new ProductDetailPageViewModel()
            //{
            //    ProductName = name,
            //    ProductPSIN = psin,
            //    Description = "This is a nice test product",
            //    UnitsAvailable = 12,
            //    Price = 18.99m,
                
            //};

            var apiResponse = await _requestManager.Send<string, BaseApiResponseDto<ProductDetailPageResponseDto>>($"/api/Products/{psin}", null,
                HttpMethod.Get);

            // ProductDetailPageViewModel model = apiResponse.Data as ProductDetailPageViewModel;

            var data = apiResponse.Data;
            var model = new ProductDetailPageViewModel()
            {
                ProductName = data.ProductName,
                Description = data.Description,
                ImageUrl = data.ImageUrl,
                GalleryImgUrls = data.GalleryImgUrls,
                ReviewCount = data.ReviewCount,
                UnitsAvailable = data.UnitsAvailable,
                ProductPSIN = data.ProductPSIN,
                Price = data.Price,
                CategoryId = data.CategoryId,
                DiscountedPrice = data.DiscountedPrice,
                CategoryName = data.CategoryName,
                SellingPrice = data.IsOnSale ? data.DiscountedPrice : data.Price,
                FiveStarRatingCount = data.FiveStarRatingCount,
                FourStarRatingCount = data.FourStarRatingCount,
                ThreeStarRatingCount = data.ThreeStarRatingCount,
                TwoStarRatingCount = data.TwoStarRatingCount,
                OneStarRatingCount = data.OneStarRatingCount,
                IsOnSale = data.IsOnSale,
                Rating = data.Rating,
                UrlFriendlyProductName = data.UrlFriendlyProductName,
                RelatedProducts = data.RelatedProducts,
                Reviews = data.Reviews
            }; 

            return View(model);
        }
    }
}
