using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO
{
    public class OrdersDTO
    {
        public int OrderID { get; set; }
        public int CarID { get; set; }
        public int GarageID { get; set; }
        public string TimeCreate { get; set; }
        public string TimeUpdate { get; set; }
        public string Status { get; set; }
        public virtual UsersDTO User { get; set; }
        public virtual OrderDetailDTO OrderDetail { get; set; }
    }
}
