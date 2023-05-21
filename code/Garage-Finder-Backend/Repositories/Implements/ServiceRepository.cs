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
    public class ServiceRepository : IServiceRepository
    {
        public void DeleteService(int id)
        {
            ServiceDAO.Instance.DeleteService(id);
        }

        public ServiceDTO GetServiceById(int id)
        {
            return Mapper.mapToDTO(ServiceDAO.Instance.FindServiceById(id));
        }
        public List<ServiceDTO> GetServices()
        {
            return ServiceDAO.Instance.GetServices().Select(p => Mapper.mapToDTO(p)).ToList();
        }
        public List<ServiceDTO> GetServicesByCategory(int id)
        {
            return ServiceDAO.Instance.GetServices().Where(c => c.CategoryID == id).Select(p => Mapper.mapToDTO(p)).ToList();
        }

        public void SaveService(ServiceDTO p)
        {
            ServiceDAO.Instance.SaveService(Mapper.mapToEntity(p));
        }

        public void UpdateService(ServiceDTO p)
        {
            ServiceDAO.Instance.UpdateService(Mapper.mapToEntity(p));
        }
    }
}
