using AutoMapper;
using DataAccess.DTO;
using DataAccess.DTO.RequestDTO.UserDTO;
using DataAccess.DTO.ResponeModels.User;
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
        private readonly IPhoneVerifyService _phoneVerifyService;
        private readonly IMapper _mapper;
        #endregion

        public UserService(IOptionsSnapshot<JwtSettings> jwtSettings,
            IUsersRepository usersRepository, IMapper mapper, IPhoneVerifyService phoneVerifyService)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepository = usersRepository;
            _mapper = mapper;
            _phoneVerifyService = phoneVerifyService;
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

        public void UpdateUser(UserUpdateDTO usersDTO, int userID)
        {
            try
            {
                var userUpdate = _userRepository.GetUserByID(userID);
                userUpdate.Name = usersDTO.Name;
                userUpdate.PhoneNumber = usersDTO.PhoneNumber;
                userUpdate.EmailAddress = usersDTO.EmailAddress;
                userUpdate.DistrictId = usersDTO.DistrictId;
                userUpdate.ProvinceId = usersDTO.ProvinceId;
                userUpdate.AddressDetail = usersDTO.AddressDetail;
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
                var user = _userRepository.GetUsersByPhone(phoneNumber);
                if(user == null)
                {
                    throw new Exception("Can't find phone number");
                }
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
    }
}
