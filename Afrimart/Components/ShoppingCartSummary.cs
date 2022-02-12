using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Afrimart.Dto;
using Afrimart.Dto.Carts;
using Afrimart.ViewModels.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceHelper.Requests;

namespace Afrimart.Components
{
    public class ShoppingCartSummary: ViewComponent
    {
        private readonly IRequestManager _requestManager;


        public ShoppingCartSummary(IRequestManager requestManager)
        {
            _requestManager = requestManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var items = _shoppingCart.GetShoppingCartItems();
            //// var items = new List<ShoppingCartItem>() { new ShoppingCartItem(), new ShoppingCartItem() };
            //_shoppingCart.ShoppingCartItems = items;

            //var shoppingCartViewModel = new ShoppingCartViewModel
            //{
            //    ShoppingCart = _shoppingCart,
            //    ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            //};

            var model = new CartPartialViewModel();
            string cartId = GetCartIdIfExists();

            if (string.IsNullOrWhiteSpace(cartId) == false)
            {
                var apiResponse = await _requestManager.Send<string, BaseApiResponseDto<ShoppingCartResponseDto>>($"/api/Cart/{cartId}", null,
                    HttpMethod.Get);

                model = new CartPartialViewModel()
                {
                    CartItems = apiResponse.Data.CartItems,
                    CartAmount = apiResponse.Data.CartAmount
                };
            }

           

            //var model = new CartPartialViewModel()
            //{
            //    CartAmount = 254.78m,
            //    CartItems = new List<CartItemDto>()
            //    {
            //        new CartItemDto()
            //        {
            //            ProductName = "Test this product and see",
            //            ProductPSIN = "ADKJD",
            //            UnitPrice = 200m,
            //            ProductImageUri = "https://source.unsplash.com/random/250x150?sig=2323",
            //            Quantity = 23
            //        },
            //        new CartItemDto()
            //        {
            //            ProductName = "Test this product and see",
            //            ProductPSIN = "ADKJD",
            //            UnitPrice = 200m,
            //            ProductImageUri = "https://source.unsplash.com/random/250x150?sig=23233",
            //            Quantity = 23
            //        },
            //        new CartItemDto()
            //        {
            //            ProductName = "Test this product and see",
            //            ProductPSIN = "ADKJD",
            //            UnitPrice = 200m,
            //            ProductImageUri = "https://source.unsplash.com/random/250x150?sig=23232",
            //            Quantity = 3
            //        },
            //        new CartItemDto()
            //        {
            //            ProductName = "Test this product and see",
            //            ProductPSIN = "ADKJD",
            //            UnitPrice = 200m,
            //            ProductImageUri = "https://source.unsplash.com/random/250x150?sig=232354",
            //            Quantity = 11
            //        },
            //        new CartItemDto()
            //        {
            //            ProductName = "Test this product and see",
            //            ProductPSIN = "ADKJD",
            //            UnitPrice = 200m,
            //            ProductImageUri = "https://source.unsplash.com/random/250x150?sig=23230",
            //            Quantity = 1
            //        }

            //    }
            //};
            return View(model);
        }
        public string GetCartIdIfExists()
        {

            var cartId = HttpContext.Session.GetString(AfrimartConstants.CART_ID_SESSION_KEY);
           
            return cartId;
        }
    }
}
