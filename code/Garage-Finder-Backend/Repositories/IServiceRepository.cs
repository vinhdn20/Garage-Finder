using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IServiceRepository
    {
        void SaveService(ServiceDTO p);
        List<ServiceDTO> GetServices();
        List<ServiceDTO> GetServicesByCategory(int id);
        ServiceDTO GetServiceById(int id);
        void UpdateService(ServiceDTO p);
        void DeleteService(int id);
    }
}
