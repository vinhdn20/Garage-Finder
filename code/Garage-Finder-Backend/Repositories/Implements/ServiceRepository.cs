﻿using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO;
using GFData.Models.Entity;
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
        private readonly IMapper _mapper;
        public ServiceRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void DeleteService(int id)
        {
            ServiceDAO.Instance.DeleteService(id);
        }

        public ServiceDTO GetServiceById(int id)
        {
            return _mapper.Map<Service, ServiceDTO>(ServiceDAO.Instance.FindServiceById(id));
        }
        public List<ServiceDTO> GetServices()
        {
            return ServiceDAO.Instance.GetServices().Select(p => _mapper.Map<Service, ServiceDTO>(p)).ToList();
        }
        public List<ServiceDTO> GetServicesByCategory(int id)
        {
            return ServiceDAO.Instance.GetServices().Where(c => c.CategoryGarageID == id).Select(p => _mapper.Map<Service, ServiceDTO>(p)).ToList();
        }

        public void SaveService(ServiceDTO p)
        {
            ServiceDAO.Instance.SaveService(_mapper.Map<ServiceDTO, Service>(p));
        }

        public void UpdateService(ServiceDTO p)
        {
            ServiceDAO.Instance.UpdateService(_mapper.Map<ServiceDTO, Service>(p));
        }
    }
}
