using DataAccess.DTO;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Util
{
    public class Mapper
    {
        public static UsersDTO mapToDTO(Users users)
        {
            if (users != null)
            {
                UsersDTO usersDTO = new UsersDTO
                {
                    UserID = users.UserID,
                    Name = users.Name,
                    Birthday = users.Birthday,
                    PhoneNumber = users.PhoneNumber,
                    EmailAddress = users.EmailAddress,
                    Password = users.Password,
                    RoleID = users.RoleID,
                    //roleName = users.RoleName,
                };
                return usersDTO;
            }
            else
            {
                return null;
            }

        }

        public static Users mapToEntity(UsersDTO usersDTO)
        {
            Users users = new Users
            {
                UserID = usersDTO.UserID,
                Name = usersDTO.Name,
                Birthday = usersDTO.Birthday,
                PhoneNumber = usersDTO.PhoneNumber,
                EmailAddress = usersDTO.EmailAddress,
                Password = usersDTO.Password,
                RoleID = usersDTO.RoleID,
                //RoleName = usersDTO.roleName,
            };

            return users;
        }
    }
}
