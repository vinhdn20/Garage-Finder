using Garage_Finder_Backend.Services.AuthService;
using Garage_Finder_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Repositories.Interfaces;
using DataAccess.DTO;
using RestSharp;
using Garage_Finder_Backend.Models.RequestModels;
using Services.GgService;
using Services.PhoneVerifyService;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Properties
        private readonly JwtSettings _jwtSettings;
        private readonly JwtService _jwtService = new JwtService();
        private readonly IUsersRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IRoleNameRepository _roleNameRepository;
        private readonly IPhoneVerifyService _phoneVerifyService;
        #endregion

        public UserController(IOptionsSnapshot<JwtSettings> jwtSettings,
            IUsersRepository usersRepository, IRefreshTokenRepository refreshTokenRepository,
            IRoleNameRepository roleNameRepository, IPhoneVerifyService phoneVerifyService)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepository = usersRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _roleNameRepository = roleNameRepository;
            _phoneVerifyService = phoneVerifyService;
        }

        [HttpGet("get")]
        [AllowAnonymous]
        public IActionResult Get()
        {
            UsersDTO user = new UsersDTO();
            return Ok(user);
        }
        #region Login/Logout
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult LoginAsync([FromBody] LoginModel loginModel)
        {
            try
            {
                var usersDTO = _userRepository.Login(loginModel.Email, loginModel.Password);
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
                return NotFound(ex.Message);
            }

        }

        [HttpPost("login-gg")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginGGAsync(string accessToken)
        {
            try
            {
                dynamic objUserInfor = GoogleService.GetUserInforByAccessTokenAsync(accessToken).Result;
                string email = objUserInfor.email;
                var usersDTO = _userRepository.GetAll().Find(x => x.EmailAddress.Equals(email));
                if (usersDTO != null)
                {
                    var roleName = _roleNameRepository.GetUserRole(usersDTO.RoleID);
                    var gfAccessToken = _jwtService.GenerateJwt(usersDTO, roleName, _jwtSettings);
                    usersDTO.AccessToken = gfAccessToken;
                    usersDTO.roleName = roleName;

                    var refreshToken = _jwtService.GenerateRefreshToken(_jwtSettings, usersDTO.UserID);
                    _refreshTokenRepository.AddOrUpdateToken(refreshToken);
                    SetRefreshToken(refreshToken);
                    return Ok(usersDTO);
                }
                else
                {
                    var userDTO = new UsersDTO()
                    {
                        EmailAddress = email,
                        RoleID = Constants.ROLE_CAR
                    };
                    _userRepository.Register(userDTO);
                    var roleName = _roleNameRepository.GetUserRole(usersDTO.RoleID);
                    var gfAccessToken = _jwtService.GenerateJwt(usersDTO, roleName, _jwtSettings);
                    usersDTO.AccessToken = gfAccessToken;
                    usersDTO.roleName = roleName;

                    var refreshToken = _jwtService.GenerateRefreshToken(_jwtSettings, usersDTO.UserID);
                    _refreshTokenRepository.AddOrUpdateToken(refreshToken);
                    SetRefreshToken(refreshToken);
                    return Ok(userDTO);
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

        [HttpGet("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            try
            {
                var jsonUser = User.FindFirstValue("user");
                var user = JsonConvert.DeserializeObject<UsersDTO>(jsonUser);
                _refreshTokenRepository.DeleteRefreshToken(user.UserID);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }

        [HttpPost]
        [Route("forgot")]
        public IActionResult ForgotPassword([FromBody] ForgotPassModel forgotPassModel)
        {
            try
            {
                if(_phoneVerifyService.VerifyPhoneNumber(forgotPassModel.verifyCode, forgotPassModel.phoneNumber).Result)
                {
                    var userDTO = _userRepository.GetUsersByPhone(forgotPassModel.phoneNumber);
                    userDTO.Password = forgotPassModel.newPassword;
                    _userRepository.Update(userDTO);
                    return Ok();
                }
                return StatusCode(500, "Can't verify code");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("sendPhoneCode")]
        public IActionResult SendPhoneCode([FromBody] string phoneNumber)
        {
            try
            {
                if (_phoneVerifyService.SendCodeAsync(phoneNumber).Result)
                {
                    return Ok();
                }
                return StatusCode(500, "Can't send code");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion  
        #region Register
        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] UserRegister registerUser)
        {
            try
            {
                UsersDTO userDTO = new UsersDTO();
                userDTO.Name = registerUser.Name;
                userDTO.Birthday = registerUser.BirthDay;
                userDTO.PhoneNumber = registerUser.PhoneNumber;
                userDTO.EmailAddress = registerUser.EmailAddress;
                userDTO.Password = registerUser.Password;
                if(registerUser.RoleID != 0)
                {
                    userDTO.RoleID = registerUser.RoleID;
                }
                else
                {
                    userDTO.RoleID = 2;
                }

                _userRepository.Register(userDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
