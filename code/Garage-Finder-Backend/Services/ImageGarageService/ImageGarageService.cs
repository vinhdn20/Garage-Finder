using AutoMapper;
using DataAccess.DTO.Garage;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ImageGarageService
{
    public class ImageGarageService : IImageGarageService
    {
        private readonly IMapper _mapper;
        private readonly IImageGarageRepository _imageGarageRepository;
        public ImageGarageService(IMapper mapper, IImageGarageRepository imageGarageRepository)
        {
            _mapper = mapper;
            _imageGarageRepository = imageGarageRepository;
        }
        public void AddImageGarage(ImageGarageDTO imageGarageDTO)
        {
            _imageGarageRepository.AddImageGarage(imageGarageDTO);
        }

        public void RemoveImageGarage(int id)
        {
            _imageGarageRepository.RemoveImageGarage(id);
        }
    }
}
