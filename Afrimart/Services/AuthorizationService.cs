using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Extensions;
using Microsoft.AspNetCore.Http;

namespace Afrimart.Services
{
    public interface IAuthorizationService
    {
        Task WriteCustomClaimsAsync(List<Claim> claims);
        Task LogoutAsync();
    }
    public class AuthorizationService: CookieAuthorizationService, IAuthorizationService
    { 
        public AuthorizationService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor, claimType: ClaimTypes.Name)
        {
        }
    }
}
