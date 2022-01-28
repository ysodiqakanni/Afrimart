using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Extensions
{
    public class CookieAuthorizationService
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _claimType;

        public CookieAuthorizationService(IHttpContextAccessor httpContextAccessor, string claimType)
        {
            _httpContextAccessor = httpContextAccessor;
            _claimType = claimType;
        }

        public string GetClaimValue(string claimType)
        {

            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated || !_httpContextAccessor.HttpContext.User.Claims.Any())
            {
                throw new AuthenticationException("User is not logged in");
            }

            return GetClaimValue(_httpContextAccessor.HttpContext.User.Claims, claimType);
        }

        public async Task WriteCustomClaimsAsync(List<Claim> claims)
        {
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            var principal = new ClaimsPrincipal(claimsIdentity);
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            //set the user for the current session so it's not missing until next request
            _httpContextAccessor.HttpContext.User = principal;
            Thread.CurrentPrincipal = principal;
        }

        public async Task LoginAsync(List<Claim> claims)
        {
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            var principal = new ClaimsPrincipal(claimsIdentity);
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            //set the user for the current session so it's not missing until next request
            _httpContextAccessor.HttpContext.User = principal;
            Thread.CurrentPrincipal = principal;
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }

        protected string GetClaimValue(IEnumerable<Claim> claims, string claimType)
        {
            return claims.First(x => x.Type == claimType).Value;
        }
    }
}
