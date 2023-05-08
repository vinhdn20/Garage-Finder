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
using Services.Models;
using AutoMapper;
using Repositories;

namespace Garage_Finder_Backend.Controllers
{
    public class UserController : Controller
    {
        #region Properties
        private readonly JwtSettings _jwtSettings;
        private readonly JwtService _jwtService = new JwtService();
        private readonly UserService _userService = new UserService();
        private readonly IUsersRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        public UserController(IOptionsSnapshot<JwtSettings> jwtSettings,
            IUsersRepository usersRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepository = usersRepository;
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
            try
            {
                var usersDTO = _userRepository.Login(loginModel.Email, loginModel.Password);
                var accessToken = _jwtService.GenerateJwt(usersDTO, _jwtSettings);
                usersDTO.AccessToken = accessToken;

                var refreshToken = _jwtService.GenerateRefreshToken(_jwtSettings);
                SetRefreshToken(refreshToken);
                return Ok(usersDTO);
            }
            catch (Exception ex)
            {
                return NotFound("Not found");
            }

        }
        /* [HttpPost("login")]
         public IActionResult Login(UsersDTO usersDTO)
         {
             try
             {
                 UsersDTO user = userRepository.Login(usersDTO.EmailAddress, usersDTO.Password);


                 return Ok(new
                 {

                 });

             }
             catch (Exception e)
             {
                 return BadRequest(e.Message);
             }
         }
 */
        [HttpPost]
        [Route("refresh-token")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            //Todo: Check refresh token
            //if (!_user.Equals(refreshToken))
            //{
            //    return BadRequest("wrong token");
            //}
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
            var emailAddress = User.FindFirstValue(ClaimTypes.Name);
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
