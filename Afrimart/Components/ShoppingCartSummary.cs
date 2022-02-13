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
            return View(model);
        }
        public string GetCartIdIfExists()
        {

            var cartId = HttpContext.Session.GetString(AfrimartConstants.CART_ID_SESSION_KEY);

            // what if user is logged in and has an associated cart in the db?
            // but somehow the cart ID is lost in session.
            // So...
            if (string.IsNullOrWhiteSpace(cartId))
            {
                // so cart ID will be the user ID if logged in, or null otherwise
                cartId = HttpContext.User.Identity.Name;
            }
           
            return cartId;
        }
    }
}
