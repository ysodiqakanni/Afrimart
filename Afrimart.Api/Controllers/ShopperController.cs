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
using Afrimart.Dto.Orders;
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
        private readonly ICartService _cartService;

        public ShopperController(IAfrimartAuthorizationService authorizationService, IShopperProfileService shopperProfileService, IAddressService addressService,
            ICartService cartService)
        {
            _authorizationService = authorizationService;
            _shopperProfileService = shopperProfileService;
            _addressService = addressService;
            _cartService = cartService;
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

        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder(CreateOrderRequestDto orderRequest)
        {
            var email = _authorizationService.GetUserEmail();

            //var cart = _cartService.GetCart(orderRequest.CartId);
            //if (cart == null || !cart.CartItems.Any())
            //{
            //    return Ok(new BaseApiResponseDto<string>()
            //    {
            //        Success = false,
            //        Data = "cart items not found",
            //        Message = "cart items not found",
            //    });
            //}

            //var lineItems = new List<LineItem>();
            //foreach (var cartItem in cart.CartItems)
            //{
            //    lineItems.Add(new LineItem()
            //    {
            //        Product = cartItem.Product,
            //        Quantity = cartItem.Quantity,
            //        SoldPricePerQuantity = cartItem.Product.SellingPrice
            //    });
            //}

            //var subTotal = lineItems.Sum(l => l.Product.SellingPrice);
            //var tax = subTotal * AfrimartConstants.TEXAS_TAX_PERCENTAGE;
            //var shippingCost = orderRequest.ShippingMethod == ShippingMethod.StorePickup
            //    ? 0
            //    : AfrimartConstants.MINIMUM_STANDARD_SHIPPING_COST;

            //var totalCost = subTotal + tax + shippingCost;
            //if (totalCost != orderRequest.TotalCost)
            //{
            //    // Todo: email a support person immediately
            //    return Ok(new BaseApiResponseDto<string>()
            //    {
            //        Success = false,
            //        Data = "An error occurred with your cart Items, we're currently looking into this issue",
            //        Message = "An error occurred with your cart Items, we're currently looking into this issue",
            //    });
            //}

            //var address = _shopperProfileService.GetUserAddresses(email).First(x => x.AddressType == AddressType.Shipping); 
            //// call payment gateway
            //var order = new Order()
            //{
            //    Identifier = "", // Create a method for this 
            //    LineItems = lineItems,
            //    SubTotal = subTotal,
            //    ShippingMethod = orderRequest.ShippingMethod,
            //    ShippingCost = shippingCost,
            //    Tax = tax,
            //    Status = OrderStatus.InProgress,
            //    TotalAmountPaid = totalCost,
            //    ShippedToPersonName = $"{address.FirstName} {address.LastName}",
            //    ShippedToAddress = $"{address.AddressLine1} {address.AddressLine2}, {address.City}, {address.State}, {address.ZipCode}"
            //};
            return Ok();
        }

        [HttpDelete("order")]
        public async Task<IActionResult> CancelOrder()
        {
            return Ok();
        }
    }
}
