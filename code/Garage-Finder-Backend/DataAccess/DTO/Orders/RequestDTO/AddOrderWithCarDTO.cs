using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Orders.RequestDTO
{
    public class AddOrderWithCarDTO
    {
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
        public int carId { get; set; }
        public int garageId { get; set; }
        public List<int> categoryGarageId { get; set; }
        public DateTime TimeAppointment { get; set; }
    }
}
