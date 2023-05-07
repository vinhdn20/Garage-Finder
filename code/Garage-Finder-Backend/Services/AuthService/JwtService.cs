using DataAccess.DTO;
using GFData.Models.Entity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Services.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Garage_Finder_Backend.Services.AuthService
{
    public class JwtService
    {
        public string GenerateJwt(UsersDTO user, JwtSettings jwtSettings)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.EmailAddress),
                    new Claim(ClaimTypes.Role, "Member"),
                    new Claim("user", JsonConvert.SerializeObject(user))
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddHours(Convert.ToDouble(jwtSettings.ExpirationInHours));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(JwtSettings jwtSettings)
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpriresDate = DateTime.UtcNow.AddHours(jwtSettings.RefreshTokenExpirationInHours),
                CreateDate = DateTime.UtcNow
            };
            return refreshToken;
        }
    }
}
