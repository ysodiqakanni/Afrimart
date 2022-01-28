using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Afrimart.Dto;
using Afrimart.Dto.Authentication;
using Afrimart.Dto.Users;
using Afrimart.Models;
using ServiceHelper.Requests;

namespace Afrimart.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRequestManager _requestManager;

        public AuthenticationService(IRequestManager requestManager)
        {
            _requestManager = requestManager;
        }

        public async Task<LoginResponseDto> Login(string email, string password)
        {
            var payload = new LoginRequestDto()
            {
                Email = email,
                Password = password
            };
            var cc = await _requestManager.Send<LoginRequestDto, LoginResponseDto>("/api/Authentication/Login", payload,
                HttpMethod.Post);
            return cc;
        }
        public async Task<BaseApiResponseDto<LoginResponseDto>> Register(string email, string password)
        {
            var payload = new CreateUserRequestDto()
            {
                Email = email,
                Password = password
            };
            var cc = await _requestManager.Send<CreateUserRequestDto, BaseApiResponseDto<LoginResponseDto>>("/api/Authentication/Sign-up", payload,
                HttpMethod.Post);
            return cc;
        } 
    }
}
