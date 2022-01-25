using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.Dto.Authentication;

namespace Afrimart.Services
{
    public interface ITokenService
    {
        Task<LoginResponseDto> Login(string email, string password);
    }
}
