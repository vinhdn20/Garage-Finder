using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IGarageRepository
    {
        void SaveGarage(GarageDTO p);
        void Add(GarageDTO garage);
        void Update(GarageDTO garage);
        List<GarageDTO> GetGarages();
        void DeleteGarage(int id);
        public List<GarageDTO> GetByPage(PageDTO p);
        public List<GarageDTO> GetGarageByUser(int id);
    }
}
