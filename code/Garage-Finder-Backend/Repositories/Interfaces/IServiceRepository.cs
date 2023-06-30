using DataAccess.DTO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IServiceRepository
    {
        ServiceDTO SaveService(ServiceDTO p);
        List<ServiceDTO> GetServices();
        List<ServiceDTO> GetServicesByCategoryGarage(int id);
        ServiceDTO GetServiceById(int id);
        void UpdateService(ServiceDTO p);
        void DeleteService(int id);
    }
}
