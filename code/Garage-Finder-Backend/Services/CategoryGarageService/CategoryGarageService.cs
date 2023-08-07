using AutoMapper;
using DataAccess.DTO;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CategoryGarageService
{
    public class CategoryGarageService : ICategoryGarageService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryGarageRepository _repository;
        public CategoryGarageService(IMapper mapper, ICategoryGarageRepository categoryGarageRepository)
        {
            _mapper = mapper;
            _repository = categoryGarageRepository;
        }
        public void Add(CategoryGarageDTO categoryGarageDTO)
        {
            _repository.Add(categoryGarageDTO);
        }

        public CategoryGarageDTO GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Remove(int id)
        {
            _repository.Remove(id);
        }
    }
}
