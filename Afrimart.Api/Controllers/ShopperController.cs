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

        public ShopperController(IAfrimartAuthorizationService authorizationService, IShopperProfileService shopperProfileService)
        {
            _authorizationService = authorizationService;
            _shopperProfileService = shopperProfileService; 
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
                response.ShippingAddress = ConvertToDto(addresses.First(x => x.AddressType == AddressType.Shipping));
                response.BillingAddress = ConvertToDto(addresses.First(x => x.AddressType == AddressType.Billing));
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

        private AddressDto ConvertToDto(Address address)
        {
            return new AddressDto()
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
                FirstName = address.FirstName,
                LastName = address.LastName,
                Id = address.Id
            };
        }
    }
}
