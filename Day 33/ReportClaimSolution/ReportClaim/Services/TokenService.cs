using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReportClaim.Interfaces;
using ReportClaim.Models.DTO;

namespace ReportClaim.Services
{
    public class TokenService : ITokenService
    {
        private readonly string secretKey;
        public TokenService(IConfiguration configuration)
        {
            secretKey = configuration["JWT:SecretKey"];
        }
        public async virtual Task<string> GenerateToken(UserTokenDTO user)
        {
            string _token = string.Empty;
            var _claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.GivenName, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),

                };

            var _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var _credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var _tokenDescriptor = new JwtSecurityToken(

                claims: _claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: _credentials
            );

            _token = new JwtSecurityTokenHandler().WriteToken(_tokenDescriptor);
            return _token;
        }
    }
}
