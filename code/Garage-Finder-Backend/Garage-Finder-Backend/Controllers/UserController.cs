using Garage_Finder_Backend.Services.AuthService;
using Garage_Finder_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Garage_Finder_Backend.Models.RequestModels;
using DataAccess.DTO;
using Newtonsoft.Json;
using GFData.Models.Entity;
using Repositories.Interfaces;
using RestSharp;

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
        #region Login/Logout
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult LoginAsync([FromBody] UsersDTO userLoginDTO)
        {
            try
            {
                var usersDTO = _userRepository.Login(userLoginDTO.EmailAddress, userLoginDTO.Password);
                var roleName = _roleNameRepository.GetUserRole(usersDTO.RoleID);
                var accessToken = _jwtService.GenerateJwt(usersDTO, roleName, _jwtSettings);
                usersDTO.AccessToken = accessToken;
                usersDTO.roleName = roleName;

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

        [HttpGet]
        [Route("login-gg")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginByGoogleAsync()
        {
            var url = "https://accounts.google.com/o/oauth2/v2/auth?" +
                "client_id=905743272860-1ob54jg8gffqdirppk90d41vf9atmh7o.apps.googleusercontent.com" +
                "&redirect_uri=http://localhost:49156/login-gg-infor" +
                "&response_type=code" +
                "&scope=https://www.googleapis.com/auth/userinfo.profile";
            return Redirect(url);
        }

        [HttpGet("{code}/{scope}")]
        [Route("login-gg-infor")]
        [AllowAnonymous]
        public async Task<IActionResult> AfterLoginGGAsync(string code, string scope)
        {
            var client = new RestClient();
            var request = new RestRequest($"https://oauth2.googleapis.com/token?" +
                $"client_id=905743272860-1ob54jg8gffqdirppk90d41vf9atmh7o.apps.googleusercontent.com" +
                $"&redirect_uri=http://localhost:49156/login-gg-infor" +
                $"&grant_type=authorization_code" +
                $"&code={code}" +
                $"&client_secret=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", Method.Post);
            RestResponse response = await client.ExecuteAsync(request);
            dynamic obj = JsonConvert.DeserializeObject(response.Content);
            string id_token = obj.id_token;
            request = new RestRequest($"https://oauth2.googleapis.com/tokeninfo?id_token={id_token}");
            response = await client.ExecuteAsync(request);

            return Ok(response.Content);
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
                            string token = _jwtService.GenerateJwt(usersDTO, roleName, _jwtSettings);
                            var newRefreshToken = _jwtService.GenerateRefreshToken(_jwtSettings, usersDTO.UserID);
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

        //[HttpGet]
        //[Route("test")]
        //[Authorize]
        //public IActionResult TestLogin()
        //{
        //    var emailAddress = User.FindFirstValue(ClaimTypes.Name);
        //    return Ok(emailAddress);
        //}

        private void SetRefreshToken(RefreshTokenDTO refreshToken)
        {
            var cookiesOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiresDate
            };

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookiesOptions);
        }

        [HttpGet]
        [Route("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            return Ok();
        }
        #endregion
        #region Register
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] UsersDTO userDTO)
        {
            try
            {
                _userRepository.Register(userDTO);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Register faile");
            }
        }


        #endregion
    }
}
