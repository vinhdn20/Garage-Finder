using DataAccess.DTO.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CarService
{
    public interface ICarService
    {
        CarDTO SaveCar(CarDTO p);
        List<CarDTO> GetCars();
        List<CarDTO> GetCarsByUser(int id);
        void UpdateCar(CarDTO p);
        void DeleteCar(int id);
        CarDTO GetCarById(int id);
    }
}
