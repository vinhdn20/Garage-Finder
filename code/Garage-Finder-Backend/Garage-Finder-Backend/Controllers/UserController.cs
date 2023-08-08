using AutoMapper;
using DataAccess.DTO.Token;
using DataAccess.DTO.User.RequestDTO;
using Garage_Finder_Backend.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Repositories.Interfaces;
using RestSharp;
using Services;
using Services.PhoneVerifyService;
using Services.RefreshTokenService;
using Services.StorageApi;
using Services.UserService;
using Twilio.Rest.Trunking.V1;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Properties
        private readonly JwtSettings _jwtSettings;
        private readonly JwtService _jwtService = new JwtService();
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IRoleNameRepository _roleNameRepository;
        private readonly IPhoneVerifyService _phoneVerifyService;
        private readonly IStorageCloud _storageCloud;
        private readonly IUserService _userService;
        #endregion

        public UserController(IOptionsSnapshot<JwtSettings> jwtSettings, IRefreshTokenService refreshTokenService,
            IRoleNameRepository roleNameRepository, IPhoneVerifyService phoneVerifyService, IStorageCloud storageCloud,
            IConfiguration configuration, IUserService userService, IMapper mapper)
        {
            _jwtSettings = jwtSettings.Value;
            _refreshTokenService = refreshTokenService;
            _roleNameRepository = roleNameRepository;
            _phoneVerifyService = phoneVerifyService;
            _storageCloud = storageCloud;
            _configuration = configuration;
            _userService = userService;
        }

        [HttpGet("get")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult Get()
        {
            var user = User.GetTokenInfor();
            //user = _userRepository.GetUserByID(user.UserID);
            var result = _userService.Get(user.UserID);
            return Ok(result);
        }
        [HttpPost("update")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult Update([FromBody] UserUpdateDTO usersDTO)
        {
            try
            {
                var user = User.GetTokenInfor();
                _userService.UpdateUser(usersDTO, user.UserID);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #region Login/Logout
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult LoginAsync([FromBody] LoginModel loginModel)
        {
            try
            {
                var userInfor = _userService.Login(loginModel);
                //SetRefreshToken(refreshToken);
                return Ok(userInfor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("login-gg")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginGGAsync(string accessToken)
        {
            try
            {
                var userInfor = _userService.LoginGG(accessToken);
                return Ok(userInfor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        //[Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult RefreshToken([FromBody] string refreshToken)
        {
            try
            {
                //var user = User.GetTokenInfor();
                var token = _userService.RefreshToken(refreshToken);
                return Ok(token);

            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //private void SetRefreshToken(RefreshTokenDTO refreshToken)
        //{
        //    var cookiesOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = refreshToken.ExpiresDate
        //    };

        //    Response.Cookies.Append("refreshToken", refreshToken.Token, cookiesOptions);
        //}

        [HttpGet("logout")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult Logout()
        {
            try
            {
                var user = User.GetTokenInfor();
                _userService.Logout(user.UserID);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("forgot")]
        public IActionResult ForgotPassword([FromBody] ForgotPassDTO forgotPassModel)
        {
            try
            {
                _userService.ForgotPassword(forgotPassModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("sendPhoneCode")]
        public IActionResult SendPhoneCode([FromBody] string phoneNumber)
        {
            try
            {
                if (_userService.SendPhoneCode(phoneNumber))
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
                //check when email is exist in database not allow register
                _userService.Register(registerUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        [HttpGet]
        [Route("getSASUriForUpload")]
        public IActionResult GetSASUri()
        {
            try
            {
                var sasUri = _storageCloud.CreateSASContainerUri();

                return Ok(sasUri);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("changePassword")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            try
            {
                var user = User.GetTokenInfor();
                _userService.ChangePassword(user.UserID, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword);
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private TokenInfor GenerateTokenInfor(int id, string roleName)
        {
            TokenInfor tokenInfor = new TokenInfor()
            {
                UserID = id,
                RoleName = roleName
            };
            return tokenInfor;
        }
    }
}
