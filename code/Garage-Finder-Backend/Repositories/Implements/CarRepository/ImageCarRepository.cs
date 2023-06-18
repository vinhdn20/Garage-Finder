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

namespace Repositories.Implements.CarRepository
{
    public class ImageCarRepository : IImageCarRepository
    {
        private readonly IMapper _mapper;
        public ImageCarRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void AddImage(ImageCarDTO imageCarDTO)
        {
            ImageCarDAO.Instance.Add(_mapper.Map<ImageCarDTO, ImageCar>(imageCarDTO));
        }

        public void RemoveImage(int id)
        {
            ImageCarDAO.Instance.Remove(id);
        }
    }
}
