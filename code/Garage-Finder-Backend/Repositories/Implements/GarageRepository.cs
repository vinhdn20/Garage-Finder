using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.DTO.RequestDTO;
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
        public GarageDTO Add(AddGarageDTO garage)
        {
            var garargeDTO = _mapper.Map<AddGarageDTO, GarageDTO>(garage);
            var garageAdded= GarageDAO.Instance.SaveGarage(_mapper.Map<GarageDTO, Garage>(garargeDTO));
            garargeDTO = _mapper.Map<Garage, GarageDTO>(garageAdded);
            return garargeDTO;
        }

        public List<GarageDTO> GetGarages()
        {
            return GarageDAO.Instance.GetGarages().Select(p => _mapper.Map<Garage, GarageDTO>(p)).ToList();
        }

        public List<GarageDTO> GetGaragesByID(int id)
        {
            return GarageDAO.Instance.GetGarages().Where(c => c.GarageID == id).Select(p => _mapper.Map<Garage, GarageDTO>(p)).ToList();
        }

        public void SaveGarage(GarageDTO p)
        {
            GarageDAO.Instance.SaveGarage(_mapper.Map<GarageDTO, Garage>(p));
        }

        public void Update(UpdateGarageDTO garage)
        {
            GarageDAO.Instance.UpdateGarage(_mapper.Map<UpdateGarageDTO, Garage>(garage));
        }

        public List<GarageDTO> FilterByCity(int provinceID) 
        {
            var garages = GarageDAO.Instance.GetGarages().Where(c => c.ProvinceID == provinceID).Select(p => _mapper.Map<Garage, GarageDTO>(p)).ToList();
            return garages;
        }

        public void DeleteGarage(int id)
        {
            GarageDAO.Instance.DeleteGarage(id);
        }
        public List<GarageDTO> GetByPage(PageDTO p) 
        { 
            var garages = GarageDAO.Instance.GetGarages().Select(p => _mapper.Map<Garage, GarageDTO>(p)).Skip((p.pageNumber - 1)*p.pageSize).Take(p.pageSize).ToList();
            return garages;
        }

        public List<GarageDTO> GetGarageByUser(int id)
        {
            return GarageDAO.Instance.GetByUserID(id).Select(p => _mapper.Map<Garage, GarageDTO>(p)).ToList();
        }
    }
}
