using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Orders
{
    public class OrderDetailDTO
    {
        public int OrderID { get; set; }
        public int GFOrderID { get; set; }
        public int CategoryGarageId { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeUpdate { get; set; }
        public DateTime TimeAppointment { get; set; }
        public string Status { get; set; }
        public string Content { get; set; }
        public string LicensePlates { get; set; }
        public int BrandID { get; set; }
        public string Color { get; set; }
        public string TypeCar { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public List<FileOrdersDTO> FileOrders { get; set; }
        public List<ImageOrdersDTO> ImageOrders { get; set; }
    }
}
