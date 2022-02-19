using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.Dto;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

using System.Net.Http;
using System.Threading.Tasks;
using Afrimart.Dto;
using Afrimart.Dto.Carts;
using Afrimart.ViewModels.Products;
using Microsoft.AspNetCore.Http; 
using ServiceHelper.Requests;

namespace Afrimart.Services
{
    public interface ISessionService
    {
        string GetCartIdIfExists();
    }
    public class SessionService: ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetCartIdIfExists()
        {

            var cartId = _httpContextAccessor.HttpContext.Session.GetString(AfrimartConstants.CART_ID_SESSION_KEY);

            // what if user is logged in and has an associated cart in the db?
            // but somehow the cart ID is lost in session.
            // So...
            if (string.IsNullOrWhiteSpace(cartId))
            {
                // so cart ID will be the user ID if logged in, or null otherwise
                cartId = _httpContextAccessor.HttpContext.User.Identity.Name;
            }

            return cartId;
        }
    }
}
