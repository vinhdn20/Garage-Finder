using AutoMapper;
using DataAccess.DTO.Car;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CarService
{
    public class CarService : ICarService
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        public CarService(IMapper mapper, ICarRepository carRepository)
        {
            _mapper = mapper;
            _carRepository = carRepository;
        }
        public void DeleteCar(int id)
        {
            _carRepository.DeleteCar(id);
        }

        public CarDTO GetCarById(int id)
        {
            var car = _carRepository.GetCarById(id);
            if (car != null)
            {
                return _mapper.Map<CarDTO>(car);
            }
            return null;
        }

        public List<CarDTO> GetCars()
        {
            return _carRepository.GetCars().ToList();
        }

        public List<CarDTO> GetCarsByUser(int id)
        {
            return _carRepository.GetCarsByUser(id).ToList();
        }
        public CarDTO SaveCar(CarDTO p)
        {
           var result = _carRepository.SaveCar(p);
            return result;
        }

        public void UpdateCar(CarDTO p)
        {
            _carRepository.UpdateCar(p);
        }
    }
}
