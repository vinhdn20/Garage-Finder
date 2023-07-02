using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.DTO.Garage;
using GFData.Models.Entity;
using Repositories.Interfaces;

namespace Repositories.Implements.Garage
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
            var garageAdded = GarageDAO.Instance.SaveGarage(_mapper.Map<GarageDTO, GFData.Models.Entity.Garage>(garargeDTO));
            garargeDTO = _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(garageAdded);
            return garargeDTO;
        }

        public GarageDTO AddGarageWithInfor(GarageDTO garageDTO, GarageInfoDTO garageInfoDTO, 
            List<GarageBrandDTO> garageBrandDTOs, List<CategoryGarageDTO> categoryGarageDTOs,
            List<ImageGarageDTO> imageGarageDTOs)
        {
            var garage = _mapper.Map<GarageDTO, GFData.Models.Entity.Garage>(garageDTO);
            var garageInfor = _mapper.Map<GarageInfoDTO, GarageInfo>(garageInfoDTO);
            var garageBrands = new List<GarageBrand>();
            garageBrandDTOs.ForEach(x => garageBrands.Add(_mapper.Map<GarageBrand>(x)));

            var categoryGarages = new List<CategoryGarage>();
            categoryGarageDTOs.ForEach(x => categoryGarages.Add(_mapper.Map<CategoryGarage>(x)));

            var imageGarages = new List<ImageGarage>();
            imageGarageDTOs.ForEach(x => imageGarages.Add(_mapper.Map<ImageGarage>(x)));

            var garageAdded = GarageDAO.Instance.SaveGrageWithInforAsync(garage, garageInfor, garageBrands, categoryGarages, imageGarages).Result;
            var garageAddedDTO = _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(garageAdded);
            return garageAddedDTO;
        }

        public List<GarageDTO> GetGarages()
        {
            return GarageDAO.Instance.GetGarages().Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).ToList();
        }

        public GarageDTO GetGaragesByID(int id)
        {
            return GarageDAO.Instance.GetGarages().Where(c => c.GarageID == id).Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).FirstOrDefault();
        }

        public void SaveGarage(GarageDTO p)
        {
            GarageDAO.Instance.SaveGarage(_mapper.Map<GarageDTO, GFData.Models.Entity.Garage>(p));
        }

        public void Update(UpdateGarageDTO garage)
        {
            GarageDAO.Instance.UpdateGarage(_mapper.Map<UpdateGarageDTO, GFData.Models.Entity.Garage>(garage));
        }

        public List<GarageDTO> GetGarageByProviceId(int? provinceID)
        {
            var garages = GarageDAO.Instance.GetGarages().Where(c => c.ProvinceID == provinceID).Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).ToList();
            return garages;
        }

        public void DeleteGarage(int id)
        {
            GarageDAO.Instance.DeleteGarage(id);
        }
        public List<GarageDTO> GetByPage(PageDTO p)
        {
            var garages = GarageDAO.Instance.GetGarages().Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).Skip((p.pageNumber - 1) * p.pageSize).Take(p.pageSize).ToList();
            return garages;
        }

        public List<GarageDTO> GetGarageByUser(int id)
        {
            return GarageDAO.Instance.GetByUserID(id).Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).ToList();
        }

        public List<GarageDTO> GetGarageByDistrictsID(int? id)
        {
            return GarageDAO.Instance.GetGarages().Where(c => c.DistrictsID == id).Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).ToList();
        }

        public List<GarageDTO> GetGarageByCategoryId(int? id)
        {
            return GarageDAO.Instance.GetGarages().Where(c => c.CategoryGarages.Any(p => p.CategoryID == id)).Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).ToList();
        }

        public List<GarageDTO> GetGarageByBrandId(int? id)
        {
            var garages = GarageDAO.Instance.GetGarages().Where(c => c.GarageBrands.Any(p => p.BrandID == id)).Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).ToList();
            return garages;
        }
    }
}
