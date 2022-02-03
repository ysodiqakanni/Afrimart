using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ServiceHelper.Token
{
    public interface ITokenService
    {
        string GenerateToken(List<Claim> claims, string secret, int expireMinutes = 50);
    }
    public class TokenService: ITokenService
    {
        public string GenerateToken(List<Claim> claims, string secret, int expireMinutes = 50)
        { 
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),
                 
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    SecurityAlgorithms.HmacSha256)

            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }
    }
}
