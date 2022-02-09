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
        public async Task<IActionResult> GetProductWithImageAndReviewsByPSIN(string psin)
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
