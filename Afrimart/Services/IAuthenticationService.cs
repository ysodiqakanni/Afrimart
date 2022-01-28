using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.Dto;
using Afrimart.Dto.Authentication;

namespace Afrimart.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDto> Login(string email, string password);
        Task<BaseApiResponseDto<LoginResponseDto>> Register(string email, string password);
    }
}
