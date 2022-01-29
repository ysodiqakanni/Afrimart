using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Microsoft.AspNetCore.Http;

namespace Afrimart.Common
{ 

    public interface IAfrimartAuthorizationService
    {
        Task WriteCustomClaimsAsync(List<Claim> claims);
        Task LogoutAsync();
        public string GetClaimValue(string claimType);
        string GetUserEmail();
    }

    public class AfrimartAuthorizationService : CookieAuthorizationService, IAfrimartAuthorizationService
    {
        public AfrimartAuthorizationService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor,
            claimType: ClaimTypes.Name)
        {

        }
        public string GetUserEmail()
        {
            return GetClaimValue(_httpContextAccessor.HttpContext.User.Claims, ClaimTypes.Name.ToString());
        }
    }
}
