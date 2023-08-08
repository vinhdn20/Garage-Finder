using AutoMapper;
using DataAccess.DTO;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BrandService
{
    public class BrandService : IBrandService
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;
        public BrandService(IMapper mapper, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }
        public void Add(BrandDTO brandDTO)
        {
            _brandRepository.Add(brandDTO);
        }

        public void Delete(int id)
        {
            _brandRepository.Delete(id);
        }

        public List<BrandDTO> GetBrand()
        {
            List<BrandDTO> brands = new List<BrandDTO>();
            var list = _brandRepository.GetBrand();
            foreach (var br in list)
            {
                var brDTO = _mapper.Map<BrandDTO>(br);
                brDTO.BrandID = br.BrandID;
                brDTO.BrandName = br.BrandName;
                brands.Add(brDTO);
            }
            return brands;
        }

        public void Update(BrandDTO brandDTO)
        {
            _brandRepository.Update(brandDTO);
        }
    }
}
