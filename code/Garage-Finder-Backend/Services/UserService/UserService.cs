using DataAccess.DTO;
using DataAccess.DTO.RequestDTO.UserDTO;
using Garage_Finder_Backend.Services.AuthService;
using GFData.Models.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Repositories.Interfaces;
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
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IRoleNameRepository _roleNameRepository;
        private readonly IPhoneVerifyService _phoneVerifyService;
        private readonly IStorageCloud _storageCloud;
        public readonly IConfiguration _configuration;
        #endregion

        public UserService(IOptionsSnapshot<JwtSettings> jwtSettings,
            IUsersRepository usersRepository, IRefreshTokenRepository refreshTokenRepository,
            IRoleNameRepository roleNameRepository, IPhoneVerifyService phoneVerifyService, IStorageCloud storageCloud, IConfiguration configuration)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepository = usersRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _roleNameRepository = roleNameRepository;
            _phoneVerifyService = phoneVerifyService;
            _storageCloud = storageCloud;
            _configuration = configuration;
        }

        public void UpdateUser(UserUpdateDTO usersDTO, int userID)
        {
            try
            {
                var userUpdate = _userRepository.GetUserByID(userID);
                userUpdate.Name = usersDTO.Name;
                userUpdate.PhoneNumber = usersDTO.PhoneNumber;
                userUpdate.EmailAddress = usersDTO.EmailAddress;
                userUpdate.Address = usersDTO.Address;
                userUpdate.AddressDetail = usersDTO.AddressDetail;
                _userRepository.Update(userUpdate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
