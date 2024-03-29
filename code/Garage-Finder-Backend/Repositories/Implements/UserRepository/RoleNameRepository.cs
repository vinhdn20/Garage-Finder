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

namespace Repositories.Implements.UserRepository
{
    public class RoleNameRepository : IRoleNameRepository
    {
        private readonly IMapper _mapper;
        public RoleNameRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public RoleNameDTO GetUserRole(int userId)
        {
            return _mapper.Map<RoleName, RoleNameDTO>(RoleDAO.Instance.FindById(userId));
        }
    }
}
