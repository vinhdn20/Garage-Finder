using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUsersRepository
    {
        UsersDTO Login(string email, string password);
        UsersDTO GetUsers(string email);
        void Register(UsersDTO user);
        void Add(UsersDTO user);
        void Update(UsersDTO user);
        List<UsersDTO> GetAll();
    }
}
