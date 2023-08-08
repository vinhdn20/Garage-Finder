using AutoMapper;
using DataAccess.DTO.Category;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(IMapper mapper, ICategoryRepository categoryRepositorye)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepositorye;
        }
        public void Add(CategoryDTO categoryDTO)
        {
            _categoryRepository.Add(categoryDTO);
        }

        public void Delete(int id)
        {
            _categoryRepository.Delete(id);
        }

        public List<CategoryDTO> GetCategory()
        {
            return _categoryRepository.GetCategory().ToList();
        }

        public void Update(CategoryDTO categoryDTO)
        {
            _categoryRepository.Update(categoryDTO);
        }
    }
}
