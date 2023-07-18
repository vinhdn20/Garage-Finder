using AutoMapper;
using DataAccess.DTO;
using DataAccess.DTO.Garage;
using Mailjet.Client.Resources;
using Repositories.Implements.CategoryRepository;
using Repositories.Implements.Garage;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GarageService
{
    public class GarageService : IGarageService
    {
        private readonly IGarageRepository _garageRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        public GarageService(IGarageRepository garageRepository, IMapper mapper,
            ICategoryRepository categoryRepository, IBrandRepository brandRepository)
        {
            _garageRepository = garageRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
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
                    BrId = brand.BrandID,
                    BrandName = dbbrand.BrandName,
                    LinkImage = dbbrand.ImageLink
                });
            }
            return result;
        }
    }
}
