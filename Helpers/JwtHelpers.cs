using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Text;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using MCQPuzzleGame.Model;
using System;

namespace MCQPuzzleGame.Helpers
{
    public class JwtHelpers : IJwtHelper
    {
        private readonly IConfiguration _config;
        public JwtHelpers(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> GenerateTokens(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["JWT:key"]);
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Role, user.UserRole),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(60).ToString())
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDesc);
            return tokenHandler.WriteToken(token);
           
        }
    }
}
