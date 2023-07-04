using AutoMapper;
using DataAccess.DTO.Orders;
using DataAccess.DTO.Staff;
using DataAccess.DTO.Token;
using DataAccess.DTO.User;
using DataAccess.DTO.User.RequestDTO;
using DataAccess.DTO.User.ResponeModels;
using Garage_Finder_Backend.Services.AuthService;
using GFData.Models.Entity;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Repositories.Implements.Garage;
using Repositories.Implements.UserRepository;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.StaffService
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IGarageRepository _garageRepository;
        private readonly IStaffRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtSettings _jwtSettings;
        private readonly JwtService _jwtService = new JwtService();
        private readonly IMapper _mapper;

        public StaffService(IStaffRepository staffRepository, IGarageRepository garageRepository,
            IMapper mapper, IOptionsSnapshot<JwtSettings> jwtSettings,
            IStaffRefreshTokenRepository refreshTokenRepository)
        {
            _staffRepository = staffRepository;
            _garageRepository = garageRepository;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public StaffDTO AddStaff(AddStaffDTO addStaff, int userId)
        {
            if(!ValidationGarageOwner(addStaff.GarageID, userId))
            {
                throw new Exception("Authorize exception!");
            }

            Staff staff = _mapper.Map<Staff>(addStaff);
            var staffAdd = _staffRepository.AddStaff(staff);
            return _mapper.Map<StaffDTO>(staffAdd);
        }

        public void DeleteStaff(int id, int userId)
        {
            var staff = _staffRepository.GetById(id);
            if (!ValidationGarageOwner(staff.GarageID, userId))
            {
                throw new Exception("Authorize exception!");
            }
            _staffRepository.DeleteStaff(id);
        }

        public List<StaffDTO> GetByGarageID(int id, int userId)
        {
            if (!ValidationGarageOwner(id, userId))
            {
                throw new Exception("Authorize exception!");
            }
            var staffList = _staffRepository.GetByGarageId(id);
            var results = staffList.Select(x => _mapper.Map<StaffDTO>(x)).ToList();
            return results;
        }

        public LoginStaffDTO LoginStaff(LoginModel loginModel)
        {
            if (!loginModel.Email.IsValidEmail())
            {
                throw new Exception("Email not valid");
            }

            var staff = _staffRepository.Login(loginModel.Email, loginModel.Password);
            LoginStaffDTO loginStaff = _mapper.Map<LoginStaffDTO>(staff);
            var tokenInfor = GenerateTokenInfor(staff.StaffId, Constants.ROLE_STAFF);
            var accessToken = _jwtService.GenerateJwtForStaff(tokenInfor, _jwtSettings);
            loginStaff.AccessToken = accessToken;

            var refreshToken = _jwtService.GenerateStaffRefreshToken(_jwtSettings, staff.StaffId);
            _refreshTokenRepository.AddOrUpdateToken(refreshToken);
            loginStaff.RefreshToken = refreshToken;

            return loginStaff;
        }

        public Object RefreshToken(string refreshToken, int userId)
        {
            var userRefreshToken = _refreshTokenRepository.GetRefreshToken(userId);
            for (int i = 0; i < userRefreshToken.Count; i++)
            {
                if (userRefreshToken[i].Token.Equals(refreshToken))
                {
                    if (userRefreshToken[i].ExpiresDate < DateTime.UtcNow)
                    {
                        throw new Exception("Token expires");
                    }
                    else
                    {
                        var userDTO = _staffRepository.GetById(userId);

                        var tokenInfor = GenerateTokenInfor(userDTO.StaffId, Constants.ROLE_STAFF);
                        string token = _jwtService.GenerateJwtForStaff(tokenInfor, _jwtSettings);

                        var newRefreshToken = _jwtService.GenerateStaffRefreshToken(_jwtSettings, userDTO.StaffId);
                        newRefreshToken.TokenID = userRefreshToken[i].TokenID;
                        _refreshTokenRepository.AddOrUpdateToken(newRefreshToken);
                        return new { token, newRefreshToken };
                    }
                }
            }
            throw new Exception("Invalid refresh token");

        }

        public StaffDTO GetMyInfor(int id)
        {
            var staff = _mapper.Map<StaffDTO>(_staffRepository.GetById(id));
            return staff;
        }

        public void UpdateStaff(UpdateStaffDTO staff, int userId)
        {
            var stafDB = _staffRepository.GetById(staff.StaffId);
            if (!ValidationGarageOwner(stafDB.GarageID, userId) || userId == staff.StaffId)
            {
                throw new Exception("Authorize exception!");
            }
            var staffUpdate = _mapper.Map<Staff>(staff);
            _staffRepository.UpdateStaff(staffUpdate);
        }

        public void UpdateStatus(BlockStaffDTO blockStaff, int userId)
        {
            var stafDB = _staffRepository.GetById(blockStaff.StaffID);
            if (!ValidationGarageOwner(stafDB.GarageID, userId))
            {
                throw new Exception("Authorize exception!");
            }

            if(!Constants.STAFF_STATUS.Contains(blockStaff.Status))
            {
                throw new Exception("Status invalid");
            }
            var staff = _staffRepository.GetById(blockStaff.StaffID);
            staff.Status = blockStaff.Status;
            _staffRepository.UpdateStaff(staff);
        }

        private bool ValidationGarageOwner(int garageId, int userId)
        {
            var garages = _garageRepository.GetGarageByUser(userId);
            if (!garages.Any(x => x.GarageID == garageId))
            {
                return false;
            }
            return true;
        }

        private static TokenInfor GenerateTokenInfor(int id, string roleName)
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
