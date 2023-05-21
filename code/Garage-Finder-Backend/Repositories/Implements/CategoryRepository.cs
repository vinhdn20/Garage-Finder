using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.Util;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements
{
    public class CategoryRepository : ICategoryRepository
    {
        public void Add(CategoryDTO categoryDTO)
        {
            CategoryDAO.Instance.Add(Mapper.mapToEntity(categoryDTO));
        }

        public void Delete(int id)
        {
            CategoryDAO.Instance.DeleteCategory(id);
        }

        public List<CategoryDTO> GetCategory()
        {
            return CategoryDAO.Instance.GetCategories().Select(m => Mapper.mapToDTO(m)).ToList();
        }

        public void Update(CategoryDTO categoryDTO)
        {
            CategoryDAO.Instance.Update(Mapper.mapToEntity(categoryDTO));
        }
    }
}
