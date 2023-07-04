using DataAccess.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        List<CategoryDTO> GetCategory();
        void Add(CategoryDTO categoryDTO);
        void Update(CategoryDTO categoryDTO);
        void Delete(int id);
    }
}
