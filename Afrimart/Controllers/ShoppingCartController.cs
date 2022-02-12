using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Afrimart.Dto;
using Afrimart.Dto.Carts;
using Microsoft.AspNetCore.Http;
using ServiceHelper.Requests;

namespace Afrimart.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IRequestManager _requestManager; 
        public ShoppingCartController(IRequestManager requestManager)
        {
            _requestManager = requestManager;
        }
        // main shopping cart page
        [Route("cart")]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddToCart(string psin, int quantity=1)
        { 
            var cartId = GetCartId();
            var payload = new AddToCartRequestDto()
            {
                CartId = cartId,
                PSIN = psin,
                Count = quantity
            };
            await _requestManager.Send<AddToCartRequestDto, string>($"/api/Cart/", payload,
                HttpMethod.Post);
             
            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromCart(string psin, int quantity)
        {
            // get the cartItem by psin and cartId
            // if quantity is >= totalCount, remove the cartItem completely
            // otherwise, just reduce the quantity and update prices
            return View();
        }
        
        public IActionResult CartSummary()
        {
           
            return PartialView("_CartSummary");
        }
        public string GetCartId()
        { 

            var cartId = HttpContext.Session.GetString(AfrimartConstants.CART_ID_SESSION_KEY);
            if (string.IsNullOrWhiteSpace(cartId))
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
                {
                    cartId = HttpContext.User.Identity.Name;
                }
                else
                {
                    cartId = Guid.NewGuid().ToString();
                }
                HttpContext.Session.SetString(AfrimartConstants.CART_ID_SESSION_KEY, cartId);
            }

            return cartId;
        }
    }
}
