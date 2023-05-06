using Garage_Finder_Backend.Services.AuthService;
using Garage_Finder_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Garage_Finder_Backend.Models.RequestModels;
using Garage_Finder_Backend.Models.ResponeModels;

namespace Garage_Finder_Backend.Controllers
{
    public class UserController : Controller
    {
        #region Properties
        private readonly JwtSettings _jwtSettings;
        private readonly JwtService _jwtService = new JwtService();
        private readonly UserService _userService = new UserService();
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
                LoginResponseModel loginResponseModel = new LoginResponseModel()
                {
                    AccessToken = accessToken
                };
                return Ok(loginResponseModel);
            }
            return NotFound("Not found");
        }

        [HttpGet]
        [Route("test")]
        [Authorize(Roles = "Member")]
        public IActionResult TestLogin()
        {
            return Ok();
        }
    }
}
