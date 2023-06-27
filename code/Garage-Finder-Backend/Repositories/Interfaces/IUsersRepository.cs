using DataAccess.DTO;
using DataAccess.DTO.User.ResponeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUsersRepository
    {
        UserInfor Login(string email, string password);
        UsersDTO GetUsersByEmail(string email);
        UsersDTO GetUsersByPhone(string phone);
        UsersDTO GetUserByID(int id);
        void Register(UsersDTO user);
        void Add(UsersDTO user);
        void Update(UsersDTO user);
        List<UsersDTO> GetAll();
    }
}
