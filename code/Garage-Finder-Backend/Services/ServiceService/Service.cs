using AutoMapper;
using DataAccess.DTO;
using DataAccess.DTO.Services;
using DataAccess.DTO.Services.RequestSerivesDTO;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceService
{
    public class Service : IService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IGarageRepository _garageRepository;
        private readonly ICategoryGarageRepository _categoryGarageRepository;
        private readonly IMapper _mapper;
        public Service(IServiceRepository serviceRepository, IGarageRepository garageRepository,
            ICategoryGarageRepository categoryGarageRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _garageRepository = garageRepository;
            _categoryGarageRepository = categoryGarageRepository;
            _mapper = mapper;
        }
        public ServiceDTO AddService(AddServiceDTO serviceDTO, int userId)
        {
            var categoryGarage = _categoryGarageRepository.GetById(serviceDTO.CategoryGarageID);
            if(!CheckCategoryOwner(categoryGarage, userId))
            {
                throw new Exception("Authorize exception.");
            }
            ServiceDTO service = _mapper.Map<ServiceDTO>(serviceDTO);
            return _serviceRepository.SaveService(service);
        }

        public void DeleteServiceById(int id, int userId)
        {
            var service = _serviceRepository.GetServiceById(id);
            if (!CheckServiceOwner(service, userId))
            {
                throw new Exception("Authorize exception");
            }
            _serviceRepository.DeleteService(id);
        }

        public List<ServiceDTO> GetServceByCategoryGarageId(int id)
        {
            var categoryGarage = _categoryGarageRepository.GetById(id);
            if(categoryGarage == null)
            {
                throw new Exception($"Can not find category garage {id}");
            }
            var service = _serviceRepository.GetServicesByCategoryGarage(id);
            return service;
        }

        public ServiceDTO GetServiceById(int id)
        {
            return _serviceRepository.GetServiceById(id);
        }

        public void UpdateService(ServiceDTO service, int userId)
        {
            var oldService = _serviceRepository.GetServiceById(service.ServiceID);
            if (!CheckServiceOwner(oldService, userId))
            {
                throw new Exception("Authorize exception");
            }

            var categoryGarage = _categoryGarageRepository.GetById(service.CategoryGarageID);
            if (!CheckCategoryOwner(categoryGarage, userId))
            {
                throw new Exception("Authorize exception! You are now the owner off categoryGarage");
            }
            _serviceRepository.UpdateService(service);
        }

        private bool CheckServiceOwner(ServiceDTO service, int userId)
        {
            if(service == null)
            {
                throw new Exception($"Can not find service {service.ServiceID}");
            }
            var categoryGarage = _categoryGarageRepository.GetById(service.CategoryGarageID);
            return CheckCategoryOwner(categoryGarage, userId);
        }

        private bool CheckCategoryOwner(CategoryGarageDTO categoryGarageDTO, int userId)
        {
            if(categoryGarageDTO == null)
            {
                throw new Exception($"Can not find categorygarage {categoryGarageDTO.CategoryGarageID}");
            }
            var garages = _garageRepository.GetGarageByUser(userId);
            if (!garages.Any(x => x.GarageID == categoryGarageDTO.GarageID))
            {
                return false;
            }
            return true;
        }
    }
}
