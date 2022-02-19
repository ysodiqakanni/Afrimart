using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.Common;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto;
using Afrimart.Dto.Carts;
using Afrimart.Service.Contracts;
using Afrimart.Dto.Addresses;
using Microsoft.AspNetCore.Authorization;

namespace Afrimart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IAfrimartAuthorizationService _authorizationService;
        private readonly IShopperProfileService _shopperProfileService;
        private readonly IAddressService _addressService;

        public CartController(ICartService cartService, IAfrimartAuthorizationService authorizationService, IShopperProfileService shopperProfileService, IAddressService addressService)
        {
            _cartService = cartService;
            _authorizationService = authorizationService;
            _shopperProfileService = shopperProfileService;
            _addressService = addressService;
        }

        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCart(string cartId)
        {
            var result = GetCartSummary(cartId);

            return Ok(new BaseApiResponseDto<ShoppingCartResponseDto>()
            {
                Success = true,
                Data = result
            });
        }
         
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequestDto request)
        {
            await _cartService.AddToCart(request.CartId, request.PSIN, request.Count);

            return Ok();
        }

        [HttpDelete("{cartId},{psin}")]
        public async Task<IActionResult> RemoveProductFromCart(string cartId, string psin)
        {
            await _cartService.RemoveProductFromCart(cartId, psin);

            return Ok();
        }

        [Authorize]
        [HttpGet("checkout/{cartId}")]
        public async Task<IActionResult> GetCheckoutPageData(string cartId)
        {
            var cartSummary = GetCartSummary(cartId);

            var email = _authorizationService.GetUserEmail();
            var addressResult = new AddressListResponseDto();
            var addresses = _shopperProfileService.GetUserAddresses(email);
            if (addresses.Any())
            {
                addressResult.ShippingAddress = _addressService.ConvertToDto(addresses.First(x => x.AddressType == AddressType.Shipping));
                addressResult.BillingAddress = _addressService.ConvertToDto(addresses.First(x => x.AddressType == AddressType.Billing));
            }

            var response = new CheckoutPageDataDto()
            {
                AddressData = addressResult,
                CartSummary = cartSummary
            };

            return Ok(new BaseApiResponseDto<CheckoutPageDataDto>()
            {
                Success = true,
                Data = response
            });
        }

        private ShoppingCartResponseDto GetCartSummary(string cartId)
        {
            var cart = _cartService.GetCart(cartId);
            var result = cart == null ? new ShoppingCartResponseDto()
                : new ShoppingCartResponseDto()
                {
                    CartItems = cart.CartItems.Select(x =>
                        new CartItemDto()
                        {
                            ProductName = x.Product.Name,
                            ProductPSIN = x.Product.PSIN,
                            UnitPrice = x.Product.SellingPrice,
                            Quantity = x.Quantity,
                            ProductImageUri = x.Product.DisplayImageUri,
                            AvailableUnits = x.Product.QuantityAvailable
                        }).ToList(),
                    CartAmount = cart.CartItems.Sum(x => x.NetAmount)
                };

            return result;
        }
    }
}
