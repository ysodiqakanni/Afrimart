using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.Dto;
using Afrimart.Dto.Public;
using Afrimart.Service.Contracts;

namespace Afrimart.Api.Controllers
{
    /// <summary>
    /// These endpoints are open to the public
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        // get products with different criteria
        // get product by some iD (maybe asin)
        // search prods
        // search by category

        // get featured prods

        // get best selling
        [HttpGet("{psin}")]
        public async Task<IActionResult> GetProductWithImagesAndReviewsByPSIN(string psin)
        {
            var product = _productService.GetProductEntityWithImagesAndReviewsByPSIN(psin);
            var reviewsDto = product.Reviews.OrderByDescending(x => x.DateCreated)
                .Select(x => new ReviewCardResponseDto()
                {
                    DateCreated = x.DateCreated,
                    Rating = x.Rating,
                    Cons = x.Cons,
                    CustomerImageUri = x.ReviewerImageUri,
                    CustomerName = x.ReviewerName,
                    Pros = x.Pros,
                    Review = x.Review
                }).ToList();
            var productFileUrisDto = product.ProductFiles.Select(x => x.FileUri).ToList();
            var relatedProducts = new List<HomeProductCard>();

            var response = new ProductDetailPageResponseDto()
            {
                ProductName = product.Name,
                ReviewCount = product.ReviewCount,
                Description = product.Description,
                UnitsAvailable = product.QuantityAvailable,
                ProductPSIN = product.PSIN,
                DiscountedPrice = product.DiscountValue,
                ImageUrl = product.DisplayImageUri,
                Price = product.UnitPrice,
                CategoryId = product.ProductCategoryId,
                CategoryName = product.ProductCategory.Name,
                IsOnSale = product.IsOnSale,
                Rating = product.AverageRating,
                UrlFriendlyProductName = product.Name.Replace(" ", "-"),
                FiveStarRatingCount = 0,
                FourStarRatingCount = 0,
                ThreeStarRatingCount = 0,
                TwoStarRatingCount = 0,
                OneStarRatingCount = 0,
                SellingPrice = product.SellingPrice,

                GalleryImgUrls = productFileUrisDto,
                Reviews = reviewsDto,
                RelatedProducts = relatedProducts
            };

            return Ok(new BaseApiResponseDto<ProductDetailPageResponseDto>()
            {
                Success = true,
                Data = response
            });
        }

    }

    // Todo: move the class below away
    public class ProductDetailPageResponseDto : HomeProductCard
    {
        public decimal SellingPrice { get; set; } // could be the normal price or the Discounted price
        public int FiveStarRatingCount { get; set; }
        public int FourStarRatingCount { get; set; }
        public int ThreeStarRatingCount { get; set; }
        public int TwoStarRatingCount { get; set; }
        public int OneStarRatingCount { get; set; }
        public List<ReviewCardResponseDto> Reviews { get; set; }
        public List<HomeProductCard> RelatedProducts { get; set; }
    }

    public class ReviewCardResponseDto
    {
        public string CustomerName { get; set; }
        public string CustomerImageUri { get; set; }
        public string Review { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
