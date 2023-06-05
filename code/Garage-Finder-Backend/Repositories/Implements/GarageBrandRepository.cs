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

namespace Repositories.Implements
{
    public class GarageBrandRepository : IGarageBrandRepository
    {
        private readonly IMapper _mapper;
        public GarageBrandRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void Add(GarageBrandDTO brandDTO)
        {
            GarageBrandDAO.Instance.Add(_mapper.Map<GarageBrandDTO, GarageBrand>(brandDTO));
        }

        public void Delete(int id)
        {
            BrandDAO.Instance.Delete(id);
        }

        public List<GarageBrandDTO> GetBrand()
        {
            return GarageBrandDAO.Instance.GetBrand().Select(m => _mapper.Map<GarageBrand, GarageBrandDTO>(m)).ToList();
        }

        public void Update(GarageBrandDTO brandDTO)
        {
            GarageBrandDAO.Instance.Update(_mapper.Map<GarageBrandDTO, GarageBrand>(brandDTO));
        }
    }
}
