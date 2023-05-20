using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO
{
    public class OrdersDTO
    {
        public int OrderID { get; set; }
        public int CarID { get; set; }
        public int GarageID { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeUpdate { get; set; }
        public string Status { get; set; }
        public virtual UsersDTO User { get; set; }
        public virtual OrderDetailDTO OrderDetail { get; set; }
    }
}
