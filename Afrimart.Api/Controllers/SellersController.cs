using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Afrimart.Common;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto;
using Afrimart.Dto.Stores;
using Afrimart.Service.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace Afrimart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SellersController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly IAfrimartAuthorizationService _authorizationService;
        private readonly IUserService _userService;

        public SellersController(IStoreService storeService, IAfrimartAuthorizationService authorizationService, IUserService userService)
        {
            _storeService = storeService;
            _authorizationService = authorizationService;
            _userService = userService;
        }
        public async Task<IActionResult> Register()
        {
            return Ok();
        }
         
        [Route("store")]
        public async Task<IActionResult> CreateStore([FromBody] CreateStoreRequestDto request)
        {
            var userEmail = _authorizationService.GetUserEmail();
            var user = await _userService.GetUserByEmail(userEmail);
            
            await _storeService.CreateStore(request, user);

            return Ok("Store created and linked to user");
        }

        [HttpPost]
        [Route("products")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> AddNewProduct([FromBody] AddProductRequestDto productRequest)
        {  
            var userEmail = _authorizationService.GetUserEmail();
            var product = new Product()
            {
                Name = productRequest.Name,
                Description = productRequest.Description,
                UnitPrice = productRequest.UnitPrice,
                IsWeighted = productRequest.IsWeighted,
                MeasurementUnit = productRequest.WeightUnit,
                IsOnSale = productRequest.IsOnSale,
                DiscountValue = productRequest.SalePrice,
                DiscountPercentage =
                    Double.Parse(((100 * productRequest.SalePrice) / productRequest.UnitPrice).ToString()),
                ProductCategoryId = int.Parse(productRequest.SelectedCategory),
                SellingPrice = productRequest.DisplayPrice,
                Tags = productRequest.Tags,
                QuantityAvailable = productRequest.QuantityAvailable,
                Tax = productRequest.Tax
            };

            var result = await _storeService.AddNewProduct(product, userEmail);

            return Ok(new BaseApiResponseDto<string>()
            {
                Success = result.Item1,
                Data = result.Item2
            });
        }

        [HttpPut]
        [Route("/products")]
        public async Task<IActionResult> UpdateProduct()
        {
            return Ok();
        }

        [Route("products/{productId}/files")]
        [HttpPost]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> UploadProductImages([FromBody] List<ProductFileUploadDto> uploadRequests, int productId)
        {
            // ensure no seller is updating another seller's product
            var userEmail = _authorizationService.GetUserEmail();
            var theProduct = _storeService.GetStoreProductByIdAndEmail(productId, userEmail);
            if (theProduct == null)
            {
                return Forbid();
            }

            var prodFiles = new List<ProductFile>();
            foreach (var file in uploadRequests)
            {
                prodFiles.Add(new ProductFile()
                {
                    FileName = file.FileName,
                    FileUri = file.FileUri,
                    FileType = file.FileType,
                    ProductId = productId
                });
            }

            await _storeService.AddProductFiles(prodFiles, theProduct);
            return Ok(new BaseApiResponseDto<string>()
            {
                Success = true,
                Message = "Files saved successfully"
            });
        }

        [HttpGet("prod-list-data")]
        public async Task<IActionResult> GetProductListPageData()
        {
            var userEmail = _authorizationService.GetUserEmail();
            var data = _storeService.GetProductListData(userEmail);
            return Ok(new BaseApiResponseDto<ProductListPageDataDto>()
            {
                Success = true,
                Data = data
            });
        }

        [HttpGet("add-prod-data")]
        public async Task<IActionResult> GetProductCreationPageData()
        {
            var userEmail = _authorizationService.GetUserEmail();
            var data = _storeService.GetProductCreationPageData(userEmail);
            return Ok(new BaseApiResponseDto<AddProductPageDataDto>()
            {
                Success = true,
                Data = data
            });
        }
    }
}
