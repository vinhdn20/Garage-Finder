using DataAccess.DTO.Garage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GarageService
{
    public interface IGarageService
    {
        List<GarageDTO> GetGarageSuggest();
        void Add(AddGarageDTO addGarage, int userID);
        ViewGarageDTO GetById(int garageId);
        void DeleteGarage(int id);
        List<GarageDTO> GetGarages();
        public List<GarageDTO> GetGarageByUser(int id);
        public void Update(GarageDTO garage);
        public GarageDTO GetGaragesByID(int id);
    }
}
