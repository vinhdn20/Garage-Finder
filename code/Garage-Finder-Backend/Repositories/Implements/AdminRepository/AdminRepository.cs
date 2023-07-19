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
        public AdminRepository(IMapper mapper)
        {
            _mapper = mapper;
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

    }
}
