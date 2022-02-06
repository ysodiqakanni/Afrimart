using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;
using Afrimart.Service.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Afrimart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public AdminController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }
        // GET: api/<AdminController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // PRODUCT CATEGORIES
        [HttpPost]
        [Route("/categories")]
        public async Task<IActionResult> CreateProductCategory([FromBody] NewCategoryRequestDto request)
        {
            var category = new ProductCategory()
            {
                Name = request.Name,
                Description = request.Description,
                ParentId = request.ParentCategoryId,
                DisplayImageUri = request.ImageUri
            };

            try
            {
                await _productCategoryService.CreateProductCategory(category);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        [HttpPut]
        [Route("/categories/{id}")]
        public async Task<IActionResult> UpdateProductCategory(int id, [FromBody] NewCategoryRequestDto request)
        {
            var category = new ProductCategory()
            {
                Name = request.Name,
                Description = request.Description,
                ParentId = request.ParentCategoryId,
                DisplayImageUri = request.ImageUri
            };

            try
            {
                await _productCategoryService.UpdateProductCategory(id,category);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<AdminController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AdminController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
