using AutoMapper;
using DataAccess.DTO.Token;
using DataAccess.DTO.User;
using DataAccess.DTO.User.RequestDTO;
using DataAccess.DTO.User.ResponeModels;
using Garage_Finder_Backend.Services.AuthService;
using GFData.Models.Entity;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Repositories.Implements.UserRepository;
using Repositories.Interfaces;
using Services.GgService;
using Services.PhoneVerifyService;
using Services.StorageApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserService
{
    public class UserService : IUserService
    {
        #region Properties
        private readonly JwtSettings _jwtSettings;
        private readonly JwtService _jwtService = new JwtService();
        private readonly IUsersRepository _userRepository;
        private readonly IPhoneVerifyService _phoneVerifyService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IRoleNameRepository _roleNameRepository;
        private readonly IMapper _mapper;
        #endregion

        public UserService(IOptionsSnapshot<JwtSettings> jwtSettings,
            IUsersRepository usersRepository, IMapper mapper, IPhoneVerifyService phoneVerifyService,
            IRefreshTokenRepository refreshTokenRepository, IRoleNameRepository roleNameRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepository = usersRepository;
            _mapper = mapper;
            _phoneVerifyService = phoneVerifyService;
            _refreshTokenRepository = refreshTokenRepository;
            _roleNameRepository = roleNameRepository;
        }

        public UserInfor Get(int userId)
        {
            try
            {
                var userDTO = _userRepository.GetUserByID(userId);
                var result = _mapper.Map<UserInfor>(userDTO);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserInfor GetAll()
        {
            try
            {
                var userDTO = _userRepository.GetAll();
                var result = _mapper.Map<UserInfor>(userDTO);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateUser(UserUpdateDTO usersDTO, int userID)
        {
            try
            {
                var userUpdate = _userRepository.GetUserByID(userID);
                userUpdate = _mapper.Map(usersDTO, userUpdate);
                _userRepository.Update(userUpdate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool SendPhoneCode(string phoneNumber)
        {
            try
            {
                if (!phoneNumber.IsValidPhone())
                {
                    throw new Exception("Phone number is not valid");
                }
                //var user = _userRepository.GetUsersByPhone(phoneNumber);
                //if(user == null)
                //{
                //    throw new Exception("Can't find phone number");
                //}
                if (!_phoneVerifyService.SendCodeAsync(phoneNumber).Result)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            try
            {
                UsersDTO user = new UsersDTO();
                try
                {
                    user = _userRepository.GetUserByID(userId);
                    if (user == null)
                    {
                        throw new Exception("Can't find user");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Can't find user");
                }

                if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrEmpty(newPassword))
                {
                    throw new Exception("Password is null");
                }

                try
                {
                    _userRepository.Login(user.EmailAddress, oldPassword);
                }
                catch (Exception)
                {
                    throw new Exception("Old password is not correct");
                }

                user.Password = newPassword;
                _userRepository.Update(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Register(UserRegister registerUser)
        {
            if (!registerUser.EmailAddress.IsValidEmail())
            {
                throw new Exception("Email không hợp lệ");
            }
            if (_userRepository.GetUsersByEmail(registerUser.EmailAddress) != null)
            {
                throw new Exception("Email đã tồn tại");
            }

            UsersDTO userDTO = new UsersDTO();
            userDTO.Name = registerUser.Name;
            userDTO.PhoneNumber = registerUser.PhoneNumber;
            userDTO.EmailAddress = registerUser.EmailAddress;
            userDTO.Password = registerUser.Password;
            userDTO.RoleID = Constants.ROLE_USER_ID;
            userDTO.Status = Constants.USER_ACTIVE;

            _userRepository.Register(userDTO);
        }

        public UserInfor Login(LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.Password))
            {
                throw new Exception("Mật khẩu không được để trống");
            }
            if (string.IsNullOrEmpty(loginModel.Email))
            {
                throw new Exception("Email không được để trống");
            }
            var usersDTO = _userRepository.Login(loginModel.Email, loginModel.Password);
            if (usersDTO.Status == Constants.USER_LOCKED)
            {
                throw new Exception("Người dùng đã bị khoá tài khoản!");
            }
            var roleName = _roleNameRepository.GetUserRole(usersDTO.RoleID);
            usersDTO.roleName = roleName;
            TokenInfor tokenInfor;
            if (usersDTO.roleName.NameRole == Constants.ROLE_ADMIN)
            {
                tokenInfor = GenerateTokenInfor(usersDTO.UserID, Constants.ROLE_ADMIN);
            }
            else
            {
                tokenInfor = GenerateTokenInfor(usersDTO.UserID, Constants.ROLE_USER);
            }

            var accessToken = _jwtService.GenerateJwt(tokenInfor, roleName, _jwtSettings);
            usersDTO.AccessToken = accessToken;


            var refreshToken = _jwtService.GenerateRefreshToken(_jwtSettings, usersDTO.UserID);
            _refreshTokenRepository.AddOrUpdateToken(refreshToken);
            usersDTO.RefreshToken = refreshToken;
            return usersDTO;
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

        public UserInfor LoginGG(string accessToken)
        {
            dynamic objUserInfor = GoogleService.GetUserInforByAccessTokenAsync(accessToken).Result;
            string email = objUserInfor.email;
            var usersDTO = _userRepository.GetAll().Find(x => x.EmailAddress.Equals(email));
            if (usersDTO != null)
            {
                var userInfor = _mapper.Map<UsersDTO, UserInfor>(usersDTO);
                var roleName = _roleNameRepository.GetUserRole(userInfor.RoleID);
                var tokenInfor = GenerateTokenInfor(usersDTO.UserID, Constants.ROLE_USER);
                var gfAccessToken = _jwtService.GenerateJwt(tokenInfor, roleName, _jwtSettings);
                userInfor.AccessToken = gfAccessToken;
                userInfor.roleName = roleName;

                var refreshToken = _jwtService.GenerateRefreshToken(_jwtSettings, userInfor.UserID);
                _refreshTokenRepository.AddOrUpdateToken(refreshToken);
                userInfor.RefreshToken = refreshToken;
                //SetRefreshToken(refreshToken);
                if (userInfor.Status == Constants.USER_LOCKED)
                {
                    throw new Exception("User is locked");
                }
                else
                {
                    return userInfor;
                }
            }
            else
            {
                var userDTO = new UsersDTO()
                {
                    EmailAddress = email,
                    RoleID = Constants.ROLE_USER_ID,
                    Name = objUserInfor.given_name,
                    Status = Constants.USER_ACTIVE
                };
                _userRepository.Register(userDTO);
                usersDTO = _userRepository.GetAll().Find(x => x.EmailAddress.Equals(email));
                var userInfor = _mapper.Map<UsersDTO, UserInfor>(usersDTO);
                var roleName = _roleNameRepository.GetUserRole(userInfor.RoleID);
                var tokenInfor = GenerateTokenInfor(usersDTO.UserID, Constants.ROLE_USER);
                var gfAccessToken = _jwtService.GenerateJwt(tokenInfor, roleName, _jwtSettings);
                userInfor.AccessToken = gfAccessToken;
                userInfor.roleName = roleName;

                var refreshToken = _jwtService.GenerateRefreshToken(_jwtSettings, userInfor.UserID);
                _refreshTokenRepository.AddOrUpdateToken(refreshToken);
                userInfor.RefreshToken = refreshToken;
                //SetRefreshToken(refreshToken);
                return userInfor;
            }
        }
    }
}
