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
        void Add(AddGarageDTO addGarage, int userID);
        ViewGarageDTO GetById(int garageId);
    }
}
