﻿using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO.User;
using DataAccess.DTO.User.ResponeModels;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements.UserRepository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IMapper _mapper;
        public UsersRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void Add(UsersDTO users)
        {
            UsersDAO.Instance.SaveUser(_mapper.Map<UsersDTO, Users>(users));
        }

        public List<UsersDTO> GetAll()
        {
            return UsersDAO.Instance.FindAll().Select(m => _mapper.Map<Users, UsersDTO>(m)).ToList();
        }

        public UsersDTO GetUserByID(int id)
        {
            return _mapper.Map<Users, UsersDTO>(UsersDAO.Instance.FindUserByID(id));
        }

        public UsersDTO GetUsersByEmail(string email)
        {
            return _mapper.Map<Users, UsersDTO>(UsersDAO.Instance.FindUserByEmail(email));
        }
        public UsersDTO GetUsersByPhone(string phone)
        {
            return _mapper.Map<Users, UsersDTO>(UsersDAO.Instance.FindUserByPhone(phone));
        }

        public UserInfor Login(string email, string password)
        {
            return _mapper.Map<Users, UserInfor>(UsersDAO.Instance.FindUserByEmailPassword(email, password));
        }

        public void Register(UsersDTO user)
        {
            UsersDAO.Instance.SaveUser(_mapper.Map<UsersDTO, Users>(user));
        }

        public void Update(UsersDTO users)
        {
            UsersDAO.Instance.UpdateUser(_mapper.Map<UsersDTO, Users>(users));
        }
    }
}
