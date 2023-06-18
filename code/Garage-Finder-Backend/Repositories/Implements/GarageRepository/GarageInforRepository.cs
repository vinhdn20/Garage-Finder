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

namespace Repositories.Implements.GarageRepository.GarageRepository
{
    public class GarageInforRepository : IGarageInforRepository
    {
        private readonly IMapper _mapper;
        public GarageInforRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void Add(GarageInfoDTO garageInforDTO)
        {
            GarageInforDAO.Instance.Add(_mapper.Map<GarageInfoDTO, GarageInfo>(garageInforDTO));
        }
    }
}
