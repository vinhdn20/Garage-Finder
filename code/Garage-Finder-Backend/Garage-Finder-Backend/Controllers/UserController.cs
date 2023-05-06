using Garage_Finder_Backend.Services.AuthService;
using Garage_Finder_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Garage_Finder_Backend.Models.RequestModels;
using Garage_Finder_Backend.Models.ResponeModels;
using Garage_Finder_Backend.Models;
using DataAccess.DTO;
using Microsoft.AspNetCore.Identity;

namespace Garage_Finder_Backend.Controllers
{
    public class UserController : Controller
    {
        #region Properties
        private readonly JwtSettings _jwtSettings;
        private readonly JwtService _jwtService = new JwtService();
        private readonly UserService _userService = new UserService();
        private string _user = "";
        #endregion

        public UserController(IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult LoginAsync([FromBody] LoginModel loginModel)
        {
            if (_userService.ValidateLogin(loginModel.Username, loginModel.Password))
            {
                var accessToken = _jwtService.GenerateJwt(loginModel.Username, "Member", _jwtSettings);

                UsersDTO users = new UsersDTO()
                {
                    AccessToken = accessToken
                };

                var refreshToken = _jwtService.GenerateRefreshToken(_jwtSettings);
                SetRefreshToken(refreshToken);
                _user = refreshToken.Token;
                return Ok(users);
            }
            return NotFound("Not found");
        }

        [HttpPost]
        [Route("refresh-token")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            //Todo: Check refresh token
            if (!_user.Equals(refreshToken))
            {
                return BadRequest("wrong token");
            }
            //Todo: Generate token
            string token = "";
            var newRefreshToken = _jwtService.GenerateRefreshToken(_jwtSettings);
            SetRefreshToken(newRefreshToken);

            return Ok(token);
        }

        [HttpGet]
        [Route("test")]
        [Authorize(Roles = "Member")]
        public IActionResult TestLogin()
        {
            return Ok();
        }

        private void SetRefreshToken(RefreshToken refreshToken)
        {
            var cookiesOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpriresDate
            };

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookiesOptions);
        }
    }
}
