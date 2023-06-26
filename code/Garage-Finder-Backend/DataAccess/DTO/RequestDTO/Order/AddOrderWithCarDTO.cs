using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.RequestDTO.Order
{
    public class AddOrderWithCarDTO
    {
        public string PhoneNumber { get; set; }
        public int carId { get; set; }
        public int categoryId { get; set; }
        public DateTime DateAppoinment { get; set; }
    }
}
