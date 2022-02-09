using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.Dto;
using Afrimart.Dto.Public;
using Afrimart.Service.Contracts;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Afrimart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;

        public HomeController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }

        [HttpGet("default")]
        public async Task<IActionResult> GetHomePageData()
        { 
            var specialCategory = _productCategoryService.GetSpecialCategoryForHomePage(); 

            var response = new HomepageDataResponseDto()
            {
                SpecialCategory = new PopularCategoryCard()
                {
                    CategoryName = specialCategory.Name,
                    CategoryId = specialCategory.Id,
                    CategoryImgUri = specialCategory.DisplayImageUri,
                },
                SpecialCategoryProducts = specialCategory.Products.Select(x => new HomeProductCard()
                {
                    ProductName = x.Name,
                    Price = x.SellingPrice,
                    Description = x.Description,
                    IsOnSale = x.IsOnSale,
                    CategoryId = x.ProductCategoryId,
                    CategoryName = x.ProductCategory.Name,
                    DiscountedPrice = x.DiscountValue,
                    ImageUrl = x.DisplayImageUri,
                    ProductPSIN = x.PSIN,
                    Rating = x.AverageRating,
                    UnitsAvailable = x.QuantityAvailable,
                    GalleryImgUrls = x.ProductFiles == null? new List<string>() : x.ProductFiles
                        .Where(f => f.FileType == FileType.GalleryImages)
                        .Select(f => f.FileUri)
                        .ToList()
                }).Take(12).ToList(),
                TrendingProducts = _productService.GetTrendingProducts(8),

            };


            return Ok(new BaseApiResponseDto<HomepageDataResponseDto>()
            {
                Success = true,
                Data = response
            });
        }

        [HttpGet("product/{psin}")]
        public async Task<IActionResult> GetProductByPSIN(string psin)
        {
            var response = _productService.GetProductByPSIN(psin);
            return Ok(new BaseApiResponseDto<HomeProductCard>()
            {
                Success = true,
                Data = response
            });
        }
    }
}
