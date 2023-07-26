using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Orders.RequestDTO
{
    public class AddOrderFromGuestDTO
    {
        public int GarageId { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
        public int BrandCarID { get; set; }
        public string TypeCar { get; set; }
        public string LicensePlates { get; set; }
        public List<int> CategoryGargeId { get; set; }
        public string TimeAppointment { get; set; }
    }
}
