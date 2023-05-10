using Garage_Finder_Backend.Services.AuthService;
using Garage_Finder_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Garage_Finder_Backend.Models.RequestModels;
using DataAccess.DTO;
using Repositories;
using Newtonsoft.Json;
using GFData.Models.Entity;

namespace Garage_Finder_Backend.Controllers
{
    public class UserController : Controller
    {
        #region Properties
        private readonly JwtSettings _jwtSettings;
        private readonly JwtService _jwtService = new JwtService();
        private readonly IUsersRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IRoleNameRepository _roleNameRepository;
        #endregion

        public UserController(IOptionsSnapshot<JwtSettings> jwtSettings,
            IUsersRepository usersRepository, IRefreshTokenRepository refreshTokenRepository,
            IRoleNameRepository roleNameRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepository = usersRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _roleNameRepository = roleNameRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult LoginAsync([FromBody] UsersDTO userDTO)
        {
            try
            {
                var usersDTO = _userRepository.Login(userDTO.EmailAddress, userDTO.Password);
                var roleName = _roleNameRepository.GetUserRole(userDTO.UserID);
                var accessToken = _jwtService.GenerateJwt(usersDTO,roleName, _jwtSettings);
                usersDTO.AccessToken = accessToken;

                var refreshToken = _jwtService.GenerateRefreshToken(_jwtSettings, usersDTO.UserID);
                _refreshTokenRepository.AddOrUpdateToken(refreshToken);
                SetRefreshToken(refreshToken);
                return Ok(usersDTO);
            }
            catch (Exception ex)
            {
                return NotFound("Not found");
            }

        }

        [HttpPost]
        [Route("refresh-token")]
        public IActionResult RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];
                var jsonUser = User.FindFirstValue("user");
                var user = JsonConvert.DeserializeObject<UsersDTO>(jsonUser);
                var userRefreshToken = _refreshTokenRepository.GetRefreshToken(user.UserID);
                for (int i = 0; i < userRefreshToken.Count; i++)
                {
                    if (userRefreshToken[i].Token.Equals(refreshToken))
                    {
                        if (userRefreshToken[i].ExpiresDate < DateTime.UtcNow)
                        {
                            return Unauthorized("Token expires");
                        }
                        else
                        {
                            var usersDTO = JsonConvert.DeserializeObject<UsersDTO>(User.FindFirstValue("user"));
                            var roleName = _roleNameRepository.GetUserRole(usersDTO.UserID);
                            string token = _jwtService.GenerateJwt(usersDTO,roleName, _jwtSettings);
                            var newRefreshToken = _jwtService.GenerateRefreshToken(_jwtSettings,usersDTO.UserID);
                            newRefreshToken.TokenID = userRefreshToken[i].TokenID;
                            _refreshTokenRepository.AddOrUpdateToken(newRefreshToken);
                            SetRefreshToken(newRefreshToken);

                            return Ok(token);
                        }
                    }
                }
                return Unauthorized("Invalid refresh token");
                
            }
            catch (Exception)
            {
                return BadRequest("Refresh token not found");
            }
        }

        [HttpGet]
        [Route("test")]
        [Authorize(Roles = "Member")]
        public IActionResult TestLogin()
        {
            var emailAddress = User.FindFirstValue(ClaimTypes.Name);
            return Ok();
        }

        private void SetRefreshToken(RefreshTokenDTO refreshToken)
        {
            var cookiesOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiresDate
            };

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookiesOptions);
        }
    }
}
