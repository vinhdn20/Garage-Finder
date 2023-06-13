using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO;
using GFData.Models.Entity;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements
{
    public class ImageGarageRepository : IImageGarageRepository
    {
        private readonly IMapper _mapper;
        public ImageGarageRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void AddImageGarage(ImageGarageDTO imageGarageDTO)
        {
            ImageGarageDAO.Instance.Add(_mapper.Map<ImageGarageDTO, ImageGarage>(imageGarageDTO));
        }

        public void RemoveImageGarage(int id)
        {
            ImageGarageDAO.Instance.Remove(id);
        }
    }
}
