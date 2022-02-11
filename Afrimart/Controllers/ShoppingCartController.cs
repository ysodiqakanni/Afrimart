using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.Dto;
using Microsoft.AspNetCore.Http;

namespace Afrimart.Controllers
{
    public class ShoppingCartController : Controller
    {
        private string _cartId;
        public ShoppingCartController()
        {
            // check if there's a cartId in session
            // if not, u
                // if user is loggedIn, store cartId in session as userId or email 
                // else, store a guid
            // assign cartId to the existing or newly generated ID
            var cartId = HttpContext.Session.GetString(AfrimartConstants.CART_ID_SESSION_KEY);
            if (string.IsNullOrWhiteSpace(cartId))
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    cartId = HttpContext.User.Identity.Name;
                }
                else
                {
                    cartId = Guid.NewGuid().ToString();
                }
                HttpContext.Session.SetString(AfrimartConstants.CART_ID_SESSION_KEY, cartId);
            }

            _cartId = cartId;
        }
        // main shopping cart page
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddToCart(string psin, int quantity)
        {
            // quantity must be greater than 0 and less than 11 (1-10)
            // send to the api (params psin, quantity and cartId)
            // Create the cart if it doesn't exist
            // if it exists, increment count and update price
            return View();
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

    }
}
