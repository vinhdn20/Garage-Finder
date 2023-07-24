using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Staff
{
    public class UpdateStaffDTO
    {
        public int StaffId { get; set; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public int DistrictId { get; set; }
        public int ProvinceId { get; set; }
        public string AddressDetail { get; set; }
        public string? LinkImage { get; set; }
    }
}
