using DataAccess.DTO.RequestDTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserService
{
    public interface IUserService
    {
        void UpdateUser(UserUpdateDTO usersDTO, int userID);
        bool SendPhoneCode(string phoneNumber);
    }
}
