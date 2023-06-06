using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements
{
    public class GarageRepository : IGarageRepository
    {
        private readonly IMapper _mapper;
        public GarageRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void Add(GarageDTO garage)
        {
            GarageDAO.Instance.SaveGarage(_mapper.Map<GarageDTO, Garage>(garage));
        }

        public List<GarageDTO> GetGarages()
        {
            return GarageDAO.Instance.GetGarages().Select(p => _mapper.Map<Garage, GarageDTO>(p)).ToList();
        }

        public void SaveGarage(GarageDTO p)
        {
            GarageDAO.Instance.SaveGarage(_mapper.Map<GarageDTO, Garage>(p));
        }

        public void Update(GarageDTO garage)
        {
            GarageDAO.Instance.UpdateGarage(_mapper.Map<GarageDTO, Garage>(garage));
        }

        public void DeleteGarage(int id)
        {
            GarageDAO.Instance.DeleteGarage(id);
        }
        public List<GarageDTO> GetByPage(PageDTO p) 
        { 
            int pageSize = 3;
            var garages = GarageDAO.Instance.GetGarages().Select(p => _mapper.Map<Garage, GarageDTO>(p)).Skip((p.pageNumber - 1)*p.pageSize).Take(p.pageSize).ToList();
            return garages;
        }

        public List<GarageDTO> GetGarageByUser(int id)
        {
            return GarageDAO.Instance.GetGarages().Where(c => c.UserID == id).Select(p => _mapper.Map<Garage, GarageDTO>(p)).ToList();
        }
    }
}
