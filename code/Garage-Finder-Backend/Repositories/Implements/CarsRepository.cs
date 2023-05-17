using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements
{
    public class CarRepository : ICarRepository
    {
        public void DeleteCar(int id)
        {
            CarDAO.Instance.DeleteCar(id);
        }

        public List<CarDTO> GetCars()
        {
            return CarDAO.Instance.GetCars().Select(p => Mapper.mapToDTO(p)).ToList();
        }

        public List<CarDTO> GetCarsByUser(int id)
        {
            return CarDAO.Instance.GetCars().Where(c => c.UserID == id).Select(p => Mapper.mapToDTO(p)).ToList();
        }
        public void SaveCar(CarDTO p)
        {
            CarDAO.Instance.SaveCar(Mapper.mapToEntity(p));
        }

        public void UpdateCar(CarDTO p)
        {
            CarDAO.Instance.Update(Mapper.mapToEntity(p));
        }
    }
}
