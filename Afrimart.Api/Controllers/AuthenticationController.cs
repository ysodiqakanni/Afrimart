using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto.Authentication;
using Afrimart.Service.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace Afrimart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await _userService.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);
            if (user == null)
            {
                return Unauthorized("User not found");
            }

            var token = GenerateToken(user.Email); 
            return Ok(new LoginResponseDto()
            {
                Success = true,
                Token = token,
                User = new LoginUserDto()
                {
                    Email = user.Email,
                    FullName = $"{user.FirstName} {user.LastName}" 
                }
            });
        }

        private const string Secret = "This is a temporary secreeet that mussst beebee replced rewritten";

        private string GenerateToken(string username, int expireMinutes = 7)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }
        //public static string GenerateToken(this ClaimsIdentity identity, string secret)
        //{
        //    var now = DateTime.UtcNow;
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = identity,
        //        SigningCredentials = new SigningCredentials(
        //            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
        //            SecurityAlgorithms.HmacSha256),
        //        Expires = DateTime.Now.AddMinutes(60),
        //        NotBefore = DateTime.Now
        //    };


        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    var tokenString = tokenHandler.WriteToken(token);

        //    return tokenString;
        //}
    }
}
