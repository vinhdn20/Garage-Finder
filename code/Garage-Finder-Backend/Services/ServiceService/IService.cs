using DataAccess.DTO.Services;
using DataAccess.DTO.Services.RequestSerivesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceService
{
    public interface IService
    {
        ServiceDTO AddService(AddServiceDTO serviceDTO, int userId);
        ServiceDTO GetServiceById(int id);
        List<ServiceDTO> GetServceByCategoryGarageId(int id);
        void UpdateService(ServiceDTO service, int userId);
        void DeleteServiceById(int id, int userId);
    }
}
