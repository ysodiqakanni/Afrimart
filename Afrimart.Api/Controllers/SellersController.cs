﻿using Microsoft.AspNetCore.Http;
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

            // get loggedIn userEmail
            // get store by the email
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
    }
}
