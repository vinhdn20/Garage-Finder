using DataAccess.DTO;
using Microsoft.AspNetCore.Identity;

namespace Garage_Finder_Backend.Models
{
    public class Users : IdentityUser
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int RoleID { get; set; }
        public RoleNameDTO RoleName { get; set; }

        public string AccessToken { get; set; }
    }
}
