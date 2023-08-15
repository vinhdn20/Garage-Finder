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

        public GarageDTO AddGarageWithInfor(GarageDTO garageDTO, 
            List<GarageBrandDTO> garageBrandDTOs, List<CategoryGarageDTO> categoryGarageDTOs,
            List<ImageGarageDTO> imageGarageDTOs)
        {
            var garage = _mapper.Map<GarageDTO, GFData.Models.Entity.Garage>(garageDTO);
            var garageBrands = new List<GarageBrand>();
            garageBrandDTOs.ForEach(x => garageBrands.Add(_mapper.Map<GarageBrand>(x)));

            var categoryGarages = new List<CategoryGarage>();
            categoryGarageDTOs.ForEach(x => categoryGarages.Add(_mapper.Map<CategoryGarage>(x)));

            var imageGarages = new List<ImageGarage>();
            imageGarageDTOs.ForEach(x => imageGarages.Add(_mapper.Map<ImageGarage>(x)));

            var garageAdded = GarageDAO.Instance.SaveGrageWithInforAsync(garage, garageBrands, categoryGarages, imageGarages).Result;
            var garageAddedDTO = _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(garageAdded);
            return garageAddedDTO;
        }

        public List<GarageDTO> GetGaragesAvailable()
        {
            var garages = GarageDAO.Instance.GetGaragesAvailable().Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).ToList();
            foreach (var gara in garages)
            {
                var feedbacks = FeedbackDAO.Instance.GetByGarage(gara.GarageID);
                if(feedbacks.Count() == 0)
                {
                    gara.Star = 0;
                    gara.FeedbacksNumber = 0;
                    continue;
                }
                double sum = feedbacks.Sum(x => x.Star);
                int count = feedbacks.Count();
                gara.Star = sum / count;
                gara.FeedbacksNumber = count;
            }
            return garages;
        }

        public List<GarageDTO> GetAllGarges()
        {
            var garages = GarageDAO.Instance.GetAllGarages().Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).ToList();
            foreach (var gara in garages)
            {
                var feedbacks = FeedbackDAO.Instance.GetByGarage(gara.GarageID);
                if (feedbacks.Count() == 0)
                {
                    gara.Star = 0;
                    gara.FeedbacksNumber = 0;
                    continue;
                }
                double sum = feedbacks.Sum(x => x.Star);
                int count = feedbacks.Count();
                gara.Star = sum / count;
                gara.FeedbacksNumber = count;
            }
            return garages;
        }

        public GarageDTO GetGaragesByID(int id)
        {
            var garage = _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(GarageDAO.Instance.GetGarageByID(id));

            var feedbacks = FeedbackDAO.Instance.GetByGarage(garage.GarageID);
            if (feedbacks.Count() == 0)
            {
                garage.Star = 0;
                garage.FeedbacksNumber = 0;
                return garage;
            }
            double sum = feedbacks.Sum(x => x.Star);
            int count = feedbacks.Count();
            garage.Star = sum / count;
            garage.FeedbacksNumber = count;
            
            return garage;
        }

        public void SaveGarage(GarageDTO p)
        {
            GarageDAO.Instance.SaveGarage(_mapper.Map<GarageDTO, GFData.Models.Entity.Garage>(p));
        }

        public void Update(GarageDTO garage)
        {
            GarageDAO.Instance.UpdateGarage(_mapper.Map<GarageDTO, GFData.Models.Entity.Garage>(garage));
        }

        public List<GarageDTO> GetGarageByProviceId(int? provinceID)
        {
            var garages = GarageDAO.Instance.GetGaragesAvailable().Where(c => c.ProvinceID == provinceID).Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).ToList();
            foreach (var gara in garages)
            {
                var feedbacks = FeedbackDAO.Instance.GetByGarage(gara.GarageID);
                if (feedbacks.Count() == 0)
                {
                    gara.Star = 0;
                    gara.FeedbacksNumber = 0;
                    continue;
                }
                double sum = feedbacks.Sum(x => x.Star);
                int count = feedbacks.Count();
                gara.Star = sum / count;
            }

            return garages;
        }

        public void DeleteGarage(int id)
        {
            GarageDAO.Instance.DeleteGarage(id);
        }
        public List<GarageDTO> GetByPage(PageDTO p)
        {
            var garages = GarageDAO.Instance.GetGaragesAvailable().Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).Skip((p.pageNumber - 1) * p.pageSize).Take(p.pageSize).ToList();
            foreach (var gara in garages)
            {
                var feedbacks = FeedbackDAO.Instance.GetByGarage(gara.GarageID);
                if (feedbacks.Count() == 0)
                {
                    gara.Star = 0;
                    gara.FeedbacksNumber = 0;
                    continue;
                }
                double sum = feedbacks.Sum(x => x.Star);
                int count = feedbacks.Count();
                gara.Star = sum / count;
            }
            return garages;
        }

        public List<GarageDTO> GetGarageByUser(int id)
        {
            var garages = GarageDAO.Instance.GetByUserID(id).Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).ToList();
            foreach (var gara in garages)
            {
                var feedbacks = FeedbackDAO.Instance.GetByGarage(gara.GarageID);
                if (feedbacks.Count() == 0)
                {
                    gara.Star = 0;
                    gara.FeedbacksNumber = 0;
                    continue;
                }
                double sum = feedbacks.Sum(x => x.Star);
                int count = feedbacks.Count();
                gara.Star = sum / count;
            }
            return garages;
        }

        public List<GarageDTO> GetGarageByDistrictsID(int? id)
        {
            var garages = GarageDAO.Instance.GetGaragesAvailable().Where(c => c.DistrictsID == id).Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).ToList();
            foreach (var gara in garages)
            {
                var feedbacks = FeedbackDAO.Instance.GetByGarage(gara.GarageID);
                if (feedbacks.Count() == 0)
                {
                    gara.Star = 0;
                    gara.FeedbacksNumber = 0;
                    continue;
                }
                double sum = feedbacks.Sum(x => x.Star);
                int count = feedbacks.Count();
                gara.Star = sum / count;
            }
            return garages;
        }

        public List<GarageDTO> GetGarageByCategoryId(int? id)
        {
            var garages = GarageDAO.Instance.GetGaragesAvailable().Where(c => c.CategoryGarages.Any(p => p.CategoryID == id)).Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).ToList();
            foreach (var gara in garages)
            {
                var feedbacks = FeedbackDAO.Instance.GetByGarage(gara.GarageID);
                if (feedbacks.Count() == 0)
                {
                    gara.Star = 0;
                    gara.FeedbacksNumber = 0;
                    continue;
                }
                double sum = feedbacks.Sum(x => x.Star);
                int count = feedbacks.Count();
                gara.Star = sum / count;
            }
            return garages;
        }

        public List<GarageDTO> GetGarageByBrandId(int? id)
        {
            var garages = GarageDAO.Instance.GetGaragesAvailable().Where(c => c.GarageBrands.Any(p => p.BrandID == id)).Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).ToList();
            foreach (var gara in garages)
            {
                var feedbacks = FeedbackDAO.Instance.GetByGarage(gara.GarageID);
                if (feedbacks.Count() == 0)
                {
                    gara.Star = 0;
                    gara.FeedbacksNumber = 0;
                    continue;
                }
                double sum = feedbacks.Sum(x => x.Star);
                int count = feedbacks.Count();
                gara.Star = sum / count;
            }
            return garages;
        }
    }
}
