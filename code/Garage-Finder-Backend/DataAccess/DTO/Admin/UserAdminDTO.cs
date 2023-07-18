using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Admin
{
    public class UserAdminDTO
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public string AddressDetail { get; set; }
        public bool HaveGarage { get; set; }
        public string Status { get; set; }
    }
}
