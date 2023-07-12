using AutoMapper;
using DataAccess.DTO;
using DataAccess.DTO.Garage;
using Mailjet.Client.Resources;
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
        private readonly IMapper _mapper;
        public GarageService(IGarageRepository garageRepository, IMapper mapper)
        {
            _garageRepository = garageRepository;
            _mapper = mapper;
        }
        public void Add(AddGarageDTO addGarage, int userID)
        {

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
    }
}
