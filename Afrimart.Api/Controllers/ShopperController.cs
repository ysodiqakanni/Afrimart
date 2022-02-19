using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.Common;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto;
using Afrimart.Dto.Addresses;
using Afrimart.Service.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace Afrimart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShopperController : ControllerBase
    {
        private readonly IAfrimartAuthorizationService _authorizationService;
        private readonly IShopperProfileService _shopperProfileService;
        private readonly IAddressService _addressService;

        public ShopperController(IAfrimartAuthorizationService authorizationService, IShopperProfileService shopperProfileService, IAddressService addressService)
        {
            _authorizationService = authorizationService;
            _shopperProfileService = shopperProfileService;
            _addressService = addressService;
        }
        [HttpGet("address/status")]
        public async Task<IActionResult> GetAddressStatus()
        {
            var email = _authorizationService.GetUserEmail(); 
            var hasAddresses = _shopperProfileService.GetAddressStatus(email);
            var response = new BaseApiResponseDto<AddressStatusResponseDto>()
            {
                Success = true,
                Data = new AddressStatusResponseDto()
                {
                    AddressExists = hasAddresses
                }
            };
            return Ok(response);
        }

        [HttpGet("address")]
        public async Task<IActionResult> GetAddresses()
        {
            var email = _authorizationService.GetUserEmail();
            var response = new AddressListResponseDto();
            var addresses = _shopperProfileService.GetUserAddresses(email);
            if (addresses.Any())
            {
                response.ShippingAddress = _addressService.ConvertToDto(addresses.First(x => x.AddressType == AddressType.Shipping));
                response.BillingAddress = _addressService.ConvertToDto(addresses.First(x => x.AddressType == AddressType.Billing));
            } 

            return Ok(new BaseApiResponseDto<AddressListResponseDto>()
            {
                Success = true,
                Data = response
            });
        }

        [HttpPost("address")]
        public async Task<IActionResult> SaveOrUpdateAddresses([FromBody] SaveOrUpdateAddressDto request)
        {
            var email = _authorizationService.GetUserEmail();
             
            if (request.SameAsShipping == false && request.BillingAddress == null)
            {
                return Ok(new BaseApiResponseDto<string>()
                {
                    Success = false
                });
            }

            await _shopperProfileService.SaveOrUpdateAddresses(email, request);
            return Ok(new BaseApiResponseDto<string>()
            {
                Success = true
            });
        } 
         
    }
}
