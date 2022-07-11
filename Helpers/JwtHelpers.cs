using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using MCQPuzzleGame.Model;
using System;
using System.Security.Cryptography;

namespace MCQPuzzleGame.Helpers
{
    public class JwtHelpers : IJwtHelper
    {
        private readonly IConfiguration _config;
        public JwtHelpers(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateTokens(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["JWT:key"]);
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Role, user.UserRole ?? "user"),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(15).ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDesc);
            return tokenHandler.WriteToken(token);
           
        }
        public RefreshTokens GenerateRefreshToken(string ipAddress)
        {
            using(var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randombytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randombytes);
                return new RefreshTokens
                {
                    Token = Convert.ToBase64String(randombytes),
                    Expires = DateTime.UtcNow.AddMinutes(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress,
                };
            }
        }
    }
}
