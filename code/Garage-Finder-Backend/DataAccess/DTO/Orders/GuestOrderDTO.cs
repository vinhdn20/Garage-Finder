using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Orders
{
    public class GuestOrderDTO
    {
        public int GuestOrderID { get; set; }
        public int GFOrderID { get; set; }
        public int GarageID { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeUpdate { get; set; }
        public DateTime? TimeAppointment { get; set; }
        public string? Status { get; set; }
        public string? Content { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int? BrandCarID { get; set; }
        public string? TypeCar { get; set; }
        public string? LicensePlates { get; set; }
        public string? Name { get; set; }

        public ICollection<ImageGuestOrder>? ImageOrders { get; set; }
        public ICollection<FileGuestOrders>? FileOrders { get; set; }
        public List<GuestOrderDetail> GuestOrderDetails { get; set; }
    }
}
