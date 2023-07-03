using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO.Category;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMapper _mapper;
        public CategoryRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void Add(CategoryDTO categoryDTO)
        {
            CategoryDAO.Instance.Add(_mapper.Map<CategoryDTO, Categorys>(categoryDTO));
        }

        public void Delete(int id)
        {
            CategoryDAO.Instance.DeleteCategory(id);
        }

        public List<CategoryDTO> GetCategory()
        {
            return CategoryDAO.Instance.GetCategories().Select(m => _mapper.Map<Categorys, CategoryDTO>(m)).ToList();
        }

        public void Update(CategoryDTO categoryDTO)
        {
            CategoryDAO.Instance.Update(_mapper.Map<CategoryDTO, Categorys>(categoryDTO));
        }
    }
}
