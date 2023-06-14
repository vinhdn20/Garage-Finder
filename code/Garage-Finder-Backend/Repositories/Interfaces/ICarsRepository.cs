using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICarRepository
    {
        CarDTO SaveCar(CarDTO p);
        List<CarDTO> GetCars();
        List<CarDTO> GetCarsByUser(int id);
        void UpdateCar(CarDTO p);
        void DeleteCar(int id);

    }
}
