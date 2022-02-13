using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto;
using Afrimart.Dto.Carts;
using Afrimart.Service.Contracts;

namespace Afrimart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCart(string cartId)
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
    }
}
