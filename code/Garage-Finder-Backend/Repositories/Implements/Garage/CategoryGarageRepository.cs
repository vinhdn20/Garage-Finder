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

namespace Repositories.Implements.Garage
{
    public class CategoryGarageRepository : ICategoryGarageRepository
    {
        private readonly IMapper _mapper;
        public CategoryGarageRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void Add(CategoryGarageDTO categoryGarageDTO)
        {
            CategoryGarageDAO.Instance.Add(_mapper.Map<CategoryGarageDTO, CategoryGarage>(categoryGarageDTO));
        }

        public void Remove(int id)
        {
            CategoryGarageDAO.Instance.Remove(id);
        }
    }
}
