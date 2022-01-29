//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Extensions;
//using Microsoft.AspNetCore.Http;

//namespace Afrimart.Common
//{
//    public interface IAuthorizationService
//    {
//        Task WriteCustomClaimsAsync(List<Claim> claims);
//        Task LogoutAsync();
//        public string GetClaimValue(string claimType);
//    }
//    public class AuthorizationService: CookieAuthorizationService, IAuthorizationService
//    { 
//        public AuthorizationService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor, claimType: ClaimTypes.Name)
//        {
//        }
//    }
//}
