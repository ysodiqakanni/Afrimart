using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ServiceHelper.Requests
{
    public class RequestMessageHandler : HttpClientHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RequestMessageHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IEnumerable<Claim> claims = _httpContextAccessor.HttpContext.User.Claims;
            var token = GetLoggedInUserToken();

            if (!request.Headers.Contains("Authorization"))
            {
                request.Headers.Add("Authorization", $"Bearer {token}");
            }
             

            var response = await base.SendAsync(request, cancellationToken);

            return response;
        }

        private string GetLoggedInUserToken()
        {
            return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Token")?.Value;
        }

    }
}
