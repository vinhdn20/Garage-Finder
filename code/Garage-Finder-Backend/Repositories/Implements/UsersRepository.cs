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
    internal class UsersRepository : IUsersRepository
    {
        public void Add(UsersDTO users)
        {
            UsersDAO.Instance.SaveUser(Mapper.mapToEntity(users));
        }

        public List<UsersDTO> GetAll()
        {
            return UsersDAO.Instance.FindAll().Select(m => Mapper.mapToDTO(m)).ToList();
        }

        public UsersDTO Login(string email, string password)
        {
            return Mapper.mapToDTO(UsersDAO.Instance.FindUserByEmailPassword(email, password));
        }

        public void Update(UsersDTO users)
        {
            UsersDAO.Instance.UpdateUser(Mapper.mapToEntity(users));
        }
    }
}
