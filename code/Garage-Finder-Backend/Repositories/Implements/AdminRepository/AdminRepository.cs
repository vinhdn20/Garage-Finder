using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO.Garage;
using DataAccess.DTO.User.RequestDTO;
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
        private readonly IGarageRepository garageRepository;
        private readonly IMapper _mapper;
        public AdminRepository(IMapper mapper)
        {
            _mapper = mapper;
        }


        public List<UserAdminDTO> GetAllUser()
        {
            var garages = garageRepository.GetGarages();
            var result = UsersDAO.Instance.FindAll().Select(m => _mapper.Map<Users, UserAdminDTO>(m)).ToList();
            foreach (var user in result) 
            {
                foreach(var garage in garages)
                {
                    if (user.UserID == garage.UserID)
                    {
                        user.HaveGarage = true;
                    }
                    else 
                    {
                        user.HaveGarage = false;
                    }
                }
            }
            return result;
        }

    }
}
