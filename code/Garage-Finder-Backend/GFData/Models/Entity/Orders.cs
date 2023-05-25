using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFData.Models.Entity
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeUpdate { get; set; }
        public string Status { get; set; }
        //[NotMapped]
        //public Users User { get; set; }
        //[NotMapped]
        //public OrderDetail OrderDetail { get; set; }

        //[ForeignKey("GarageID")]
        public int GarageID { get; set; }
        public Garage Garage { get; set; }

        //[ForeignKey("CarID")]
        public int CarID { get; set; }
        public Car Car { get; set; }

        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
