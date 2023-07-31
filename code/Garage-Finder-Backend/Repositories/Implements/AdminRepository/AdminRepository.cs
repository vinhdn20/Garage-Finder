using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO.Admin;
using DataAccess.DTO.Garage;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements.AdminRepository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IMapper _mapper;
        private readonly IUsersRepository _usersRepository;
        private readonly IGarageRepository _garageRepository;
        public AdminRepository(IMapper mapper, IUsersRepository usersRepository, IGarageRepository garageRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
            _garageRepository = garageRepository;
        }

        public List<GarageDTO> GetGarages()
        {
            return GarageDAO.Instance.GetGarages().Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).ToList();
        }
        public List<UserAdminDTO> GetAllUser()
        {
            var garages = GetGarages();
            var result = UsersDAO.Instance.FindAll().Select(m => _mapper.Map<Users, UserAdminDTO>(m)).ToList();
            foreach (var user in result)
            {
                if (garages.Any(x => x.UserID == user.UserID))
                {
                    user.HaveGarage = true;
                }
                else
                {
                    user.HaveGarage = false;
                }
            }
            return result;
        }

        public void BanUser(int id)
        {
            var user = _usersRepository.GetUserByID(id);
            user.Status = Constants.USER_LOCKED;
            _usersRepository.Update(user);
        }

        public void UnBanUser(int id)
        {

            var user = _usersRepository.GetUserByID(id);
            user.Status = Constants.USER_ACTIVE;
            _usersRepository.Update(user);
        }

        public void BanGarage(int id)
        {
            var garage = _garageRepository.GetGaragesByID(id);
            garage.Status = Constants.GARAGE_LOCKED;
            _garageRepository.Update(garage);

        }

        public void SetStatusGarage(StatusGarageDTO garage)
        {
            var garages = _garageRepository.GetGaragesByID(garage.GarageID);
            garages.Status = garage.Status;
            _garageRepository.Update(garages);
        }

        public void SetStatusUser(StatusUserDTO user)
        {
            var users = _usersRepository.GetUserByID(user.UserID);
            users.Status = user.Status;
            _usersRepository.Update(users);
        }

        public void UnBanGarage(int id)
        {
            var garage = _garageRepository.GetGaragesByID(id);
            garage.Status = Constants.GARAGE_ACTIVE;
            _garageRepository.Update(garage);
        }

        public void AcceptGarage(int id)
        {
            var garage = _garageRepository.GetGaragesByID(id);
            garage.Status = Constants.GARAGE_ACTIVE;
            _garageRepository.Update(garage);

        }

        public void DeniedGarage(int id)
        {
            var garage = _garageRepository.GetGaragesByID(id);
            garage.Status = Constants.GARAGE_DENIED;
            _garageRepository.Update(garage);
        }

    }
}
