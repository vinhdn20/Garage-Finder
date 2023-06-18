using DataAccess.DTO;
using DataAccess.DTO.ResponeModels.User;
using GFData.Models.Entity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Garage_Finder_Backend.Services.AuthService
{
    public class JwtService
    {
        public string GenerateJwt(UserInfor user,RoleNameDTO roleName, JwtSettings jwtSettings)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.EmailAddress),
                    new Claim(ClaimTypes.Role, roleName.NameRole),
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

        public RefreshTokenDTO GenerateRefreshToken(JwtSettings jwtSettings, int userID)
        {
            var refreshToken = new RefreshTokenDTO()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiresDate = DateTime.UtcNow.AddHours(jwtSettings.RefreshTokenExpirationInHours),
                CreateDate = DateTime.UtcNow,
                UserID = userID
            };
            return refreshToken;
        }

        public RefreshToken GenerateRefreshToken(DateTime expriresDate, int userID)
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiresDate = expriresDate,
                CreateDate = DateTime.UtcNow,
                UserID = userID
            };
            return refreshToken;
        }
    }
}
