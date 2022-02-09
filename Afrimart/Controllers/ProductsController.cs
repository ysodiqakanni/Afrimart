using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Afrimart.Dto;
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

            var apiResponse = await _requestManager.Send<string, BaseApiResponseDto<HomeProductCard>>($"/api/Home/product/{psin}", null,
                HttpMethod.Get);

            ProductDetailPageViewModel model = apiResponse.Data as ProductDetailPageViewModel;

            if (model == null)
            {
                model = new ProductDetailPageViewModel()
                {
                    ProductName = apiResponse.Data.ProductName,
                    Description = apiResponse.Data.Description,
                    ImageUrl = apiResponse.Data.ImageUrl,
                    GalleryImgUrls = apiResponse.Data.GalleryImgUrls
                };
            }

            return View(model);
        }
    }
}
