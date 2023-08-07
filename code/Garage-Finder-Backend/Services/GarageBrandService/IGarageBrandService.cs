using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GarageBrandService
{
    public interface IGarageBrandService
    {
        List<GarageBrandDTO> GetBrand();
        void Add(GarageBrandDTO brandDTO);
        void Update(GarageBrandDTO brandDTO);
        void Delete(int id);
    }
}
