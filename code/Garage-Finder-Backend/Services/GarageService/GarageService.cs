using AutoMapper;
using DataAccess.DTO;
using DataAccess.DTO.Garage;
using Mailjet.Client.Resources;
using Repositories.Implements;
using Repositories.Implements.CategoryRepository;
using Repositories.Implements.Garage;
using Repositories.Interfaces;
using Services.SubcriptionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.GarageService
{
    public class GarageService : IGarageService
    {
        private readonly IGarageRepository _garageRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ISubscriptionRepository _subcriptionRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;
        public GarageService(IGarageRepository garageRepository, IMapper mapper,
            ICategoryRepository categoryRepository, IBrandRepository brandRepository,
            ISubscriptionRepository subcriptionRepository, IFeedbackRepository feedbackRepository)
        {
            _garageRepository = garageRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _subcriptionRepository = subcriptionRepository;
            _feedbackRepository = feedbackRepository;
        }
        public void Add(AddGarageDTO addGarage, int userID)
        {
            if (!addGarage.PhoneNumber.IsValidPhone())
            {
                throw new Exception("Phone number not valid");
            }

            if (!addGarage.EmailAddress.IsValidEmail())
            {
                throw new Exception("Email not valid");
            }
            var garargeDTO = _mapper.Map<AddGarageDTO, GarageDTO>(addGarage);
            garargeDTO.UserID = userID;
            garargeDTO.Status = Constants.GARAGE_WAITING;

            var listGarageBrand = new List<GarageBrandDTO>();
            foreach (var brand in addGarage.BrandsID)
            {
                listGarageBrand.Add(new GarageBrandDTO()
                {
                    BrandID = brand
                });
            }

            var listCategory = new List<CategoryGarageDTO>();
            foreach (var cate in addGarage.CategoriesID)
            {
                listCategory.Add(new CategoryGarageDTO()
                {
                    CategoryID = cate
                });
            }

            var listImage = new List<ImageGarageDTO>();
            foreach (var image in addGarage.ImageLink)
            {
                listImage.Add(new ImageGarageDTO()
                {
                    ImageLink = image
                });
            }
            _garageRepository.AddGarageWithInfor(garargeDTO,
                listGarageBrand, listCategory, listImage);
        }

        public ViewGarageDTO GetById(int garageId)
        {
            var garage = _garageRepository.GetGaragesByID(garageId);
            foreach (var cate in garage.CategoryGarages)
            {
                cate.CategoryName = _categoryRepository.GetCategory().Where(x => x.CategoryID == cate.CategoryID).SingleOrDefault().CategoryName;
            }
            var result = _mapper.Map<ViewGarageDTO>(garage);
            foreach (var brand in garage.GarageBrands)
            {
                var dbbrand = _brandRepository.GetBrand().Where(x => x.BrandID == brand.BrandID).SingleOrDefault();
                result.GarageBrands.Add(new ViewBrandDTO()
                {
                    BrId = brand.BrID,
                    BrandName = dbbrand.BrandName,
                    LinkImage = dbbrand.ImageLink
                });
            }
            return result;
        }

        public GarageDTO GetGaragesByID(int id)
        {
            var garage = _garageRepository.GetGarages().FirstOrDefault();

            var feedbacks = _feedbackRepository.GetListByGarage(garage.GarageID);
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

        public List<GarageDTO> GetGarageSuggest()
        {
            var garages = _garageRepository.GetGarages();
            var result = new List<GarageDTO>();
            foreach (var garage in garages)
            {
                foreach (var cate in garage.CategoryGarages)
                {
                    cate.CategoryName = _categoryRepository.GetCategory().Where(x => x.CategoryID == cate.CategoryID).SingleOrDefault().CategoryName;
                }
                if (_subcriptionRepository.GetInvoicesByGarageId(garage.GarageID).Any(x => x.ExpirationDate > DateTime.UtcNow.AddHours(7)))
                {
                    result.Add(garage);
                }
            }
            return result;
        }

        public void DeleteGarage(int id)
        {
            _garageRepository.DeleteGarage(id);
        }

        public List<GarageDTO> GetGarages()
        {
            var garages = _garageRepository.GetGarages().ToList();
            foreach (var gara in garages)
            {
                var feedbacks = _feedbackRepository.GetListByGarage(gara.GarageID);
                if (feedbacks.Count() == 0)
                {
                    gara.Star = 0;
                    gara.FeedbacksNumber = 0;
                    continue;
                }
                double sum = feedbacks.Sum(x => x.Star);
                int count = feedbacks.Count();
                gara.Star = Math.Round(sum / count,1);
                gara.FeedbacksNumber = count;
            }
            return garages;
        }

        public List<GarageDTO> GetGarageByUser(int id)
        {
            var garages = _garageRepository.GetGarageByUser(id).ToList();
            foreach (var gara in garages)
            {
                var feedbacks = _feedbackRepository.GetListByGarage(gara.GarageID);
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

        public void Update(GarageDTO garage)
        {
            _garageRepository.Update(garage);
        }
    }
}
