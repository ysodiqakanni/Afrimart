using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;
using Afrimart.Service.Contracts;

namespace Afrimart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewCategory([FromBody] NewCategoryRequestDto request)
        {
            var category = new ProductCategory()
            {
                Name = request.Name,
                Description = request.Description
            };
            await _productCategoryService.CreateNewProject(category);
            return Ok();
        }
    }
}
