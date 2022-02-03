using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto;
using Afrimart.Dto.Authentication;
using Afrimart.Dto.Users;
using Afrimart.Service.Contracts;
using Microsoft.IdentityModel.Tokens;
using ServiceHelper.Token;

namespace Afrimart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthenticationController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await _userService.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);
            if (user == null)
            {
                return Ok(new LoginResponseDto()
                {
                    Success = false
                });
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Email)
            };
            if (user.UserRoles.Any())
            {
                claims.Add(new Claim(ClaimTypes.Role, user.UserRoles.First().Role.Name));
            }
            var token = _tokenService.GenerateToken(claims, Secret); 
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

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] CreateUserRequestDto request)
        {
            if (_userService.UserExists(request.Email))
            {
                var response = new BaseApiResponseDto<LoginResponseDto>()
                {
                    Success = false,
                    Message = "The email is taken"
                };
                return Ok(response);
            }

            var user = new User()
            {
                Email = request.Email
            };
            await _userService.CreateUser(user, request.Password);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            var token = _tokenService.GenerateToken(claims, Secret); 
            return Ok(new BaseApiResponseDto<LoginResponseDto>()
            {
                Success = true,
                Data = new LoginResponseDto()
                {
                    Token = token,
                    User = new LoginUserDto()
                    {
                        Email = user.Email
                    }
                }
            });
        }

        private const string Secret = "This is a temporary secreeet that mussst beebee replced rewritten";

        private string GenerateToken1(string username, int expireMinutes = 50)
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

                //SigningCredentials = new SigningCredentials(
                //    new SymmetricSecurityKey(symmetricKey),
                //  SecurityAlgorithms.HmacSha256Signature)
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)),
                    SecurityAlgorithms.HmacSha256)
               
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
