using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Orders.RequestDTO
{
    public class AddOrderFromGuestDTO
    {
        public int GarageId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
        public int BrandCarID { get; set; }
        public string TypeCar { get; set; }
        public string LicensePlates { get; set; }
        public int CategoryGargeId { get; set; }
        public DateTime TimeAppointment { get; set; }
    }
}
