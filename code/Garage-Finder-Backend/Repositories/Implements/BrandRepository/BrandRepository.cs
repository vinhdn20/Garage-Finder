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

namespace Repositories.Implements.BrandRepository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IMapper _mapper;
        public BrandRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void Add(BrandDTO brandDTO)
        {
            BrandDAO.Instance.Add(_mapper.Map<BrandDTO, Brand>(brandDTO));
        }

        public void Delete(int id)
        {
            BrandDAO.Instance.Delete(id);
        }

        public List<BrandDTO> GetBrand()
        {
            var result = BrandDAO.Instance.GetBrand().Select(m => _mapper.Map<Brand, BrandDTO>(m)).ToList();
            return result;
        }

        public void Update(BrandDTO brandDTO)
        {
            BrandDAO.Instance.Update(_mapper.Map<BrandDTO, Brand>(brandDTO));
        }
    }
}
