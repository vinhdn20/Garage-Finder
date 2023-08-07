using AutoMapper;
using DataAccess.DTO.Admin;
using DataAccess.DTO.Garage;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IUsersRepository _usersRepository;
        private readonly IGarageRepository _garageRepository;
        private readonly IAdminRepository _adminRepository;
        public AdminService(IMapper mapper, IUsersRepository usersRepository, IGarageRepository garageRepository, IAdminRepository adminRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
            _garageRepository = garageRepository;
            _adminRepository = adminRepository;
        }

        public List<GarageDTO> GetGarages()
        {
            return _adminRepository.GetGarages().ToList();
        }
        public List<UserAdminDTO> GetAllUser()
        {
            var result = _adminRepository.GetAllUser().ToList();
            return result;
        }      
        public void SetStatusGarage(StatusGarageDTO garage)
        {
            _adminRepository.SetStatusGarage(garage);
        }

        public void SetStatusUser(StatusUserDTO user)
        {
            _adminRepository.SetStatusUser(user);
        }

        public void BanUser(int id)
        {
            throw new NotImplementedException();
        }

        public void UnBanUser(int id)
        {
            throw new NotImplementedException();
        }

        public void BanGarage(int id)
        {
            throw new NotImplementedException();
        }

        public void UnBanGarage(int id)
        {
            throw new NotImplementedException();
        }

        public void AcceptGarage(int id)
        {
            throw new NotImplementedException();
        }

        public void DeniedGarage(int id)
        {
            throw new NotImplementedException();
        }
    }
}
