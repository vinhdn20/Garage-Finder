using DataAccess.DTO.Token;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Staff
{
    public class LoginStaffDTO
    {
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public string PhoneNumber { get; set; }
        public string LinkImage { get; set; }
        public string AddressDetail { get; set; }
        public int DistrictId { get; set; }
        public int ProvinceId { get; set; }
        public string Status { get; set; }
        public string EmailAddress { get; set; }
        public string AccessToken { get; set; }
        public StaffRefreshToken RefreshToken { get; set; }
    }
}
