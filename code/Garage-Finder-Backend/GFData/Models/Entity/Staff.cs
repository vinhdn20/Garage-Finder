using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    [Index(nameof(PhoneNumber), nameof(EmailAddress), IsUnique = true)]
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }
        public string Name { get; set; }
        public string? EmployeeId { get; set; }
        public string? Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string? LinkImage { get; set; }
        public string? AddressDetail { get; set; }
        public int? DistrictId { get; set; }
        public int? ProvinceId { get; set; }
        public string? Status { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        [ForeignKey("Garage")]
        public int GarageID { get; set; }

        public Garage Garage { get; set;}
    }
}
