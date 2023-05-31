using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.Util;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements
{
    public class CarRepository : ICarRepository
    {
        private readonly IMapper _mapper;
        public CarRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void DeleteCar(int id)
        {
            CarDAO.Instance.DeleteCar(id);
        }

        public List<CarDTO> GetCars()
        {
            return CarDAO.Instance.GetCars().Select(p => _mapper.Map<Car, CarDTO>(p)).ToList();
        }

        public List<CarDTO> GetCarsByUser(int id)
        {
            return CarDAO.Instance.GetCars().Where(c => c.UserID == id).Select(p => _mapper.Map<Car, CarDTO>(p)).ToList();
        }
        public void SaveCar(CarDTO p)
        {
            CarDAO.Instance.SaveCar(_mapper.Map<CarDTO, Car>(p));
        }

        public void UpdateCar(CarDTO p)
        {
            CarDAO.Instance.Update(_mapper.Map<CarDTO, Car>(p));
        }
    }
}
