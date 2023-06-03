using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO
{
    public class OrdersDTO
    {
        public int OrderID { get; set; }
        public int CarID { get; set; }
        public int GarageID { get; set; }
        public int ServiceID { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeUpdate { get; set; }
        public DateTime TimeAppointment { get; set; }
        public string Status { get; set; }
        public string Content { get; set; }
        public string LinkFile { get; set; }
        public string ImageLink { get; set; }
    }
}
