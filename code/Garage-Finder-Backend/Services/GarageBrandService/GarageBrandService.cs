using AutoMapper;
using DataAccess.DTO;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GarageBrandService
{
    public class GarageBrandService : IGarageBrandService
    {
        private readonly IMapper _mapper;
        private readonly IGarageBrandRepository _garageBrandRepository;
        public GarageBrandService(IMapper mapper, IGarageBrandRepository garageBrandRepository)
        {
            _mapper = mapper;
            _garageBrandRepository = garageBrandRepository;
        }
        public void Add(GarageBrandDTO brandDTO)
        {
            _garageBrandRepository.Add(brandDTO);
        }

        public void Delete(int id)
        {
            _garageBrandRepository.Delete(id);
        }

        public List<GarageBrandDTO> GetBrand()
        {
            return _garageBrandRepository.GetBrand().ToList();
        }

        public void Update(GarageBrandDTO brandDTO)
        {
            _garageBrandRepository.Update(brandDTO);
        }
    }
}
