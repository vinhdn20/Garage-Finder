using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.Util;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements
{
    public class GarageRepository : IGarageRepository
    {
        public void Add(GarageDTO garage)
        {
            GarageDAO.Instance.SaveGarage(Mapper.mapToEntity(garage));
        }

        public List<GarageDTO> GetGarages()
        {
            return GarageDAO.Instance.GetGarages().Select(p => Mapper.mapToDTO(p)).ToList();
        }

        public void SaveGarage(GarageDTO p)
        {
            GarageDAO.Instance.SaveGarage(Mapper.mapToEntity(p));
        }

        public void Update(GarageDTO garage)
        {
            GarageDAO.Instance.UpdateGarage(Mapper.mapToEntity(garage));
        }

        public void DeleteGarage(int id)
        {
            GarageDAO.Instance.DeleteGarage(id);
        }
    }
}
