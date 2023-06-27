using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.User.ResponeModels
{
    public class UserInfor
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Status { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public string AddressDetail { get; set; }
        public string LinkImage { get; set; }
        public int RoleID { get; set; }
        public RoleNameDTO roleName { get; set; }

        public string AccessToken { get; set; }
        public RefreshTokenDTO RefreshToken { get; set; }
    }
}
