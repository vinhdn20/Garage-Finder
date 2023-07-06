using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Orders.ResponseDTO
{
    public class OrderDetailDTO
    {
        public int OrderID { get; set; }
        public int GFOrderID { get; set; }
        public int GarageID { get; set; }
        public List<string> Category { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeUpdate { get; set; }
        public DateTime? TimeAppointment { get; set; }
        public string? Status { get; set; }
        public string? Content { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Brand { get; set; }
        public string? TypeCar { get; set; }
        public string? LicensePlates { get; set; }
        public string? Color { get; set; }

        public string? Name { get; set; }
        public ICollection<string>? ImageOrders { get; set; }
        public ICollection<string>? FileOrders { get; set; }
    }
}
