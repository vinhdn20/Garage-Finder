using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFData.Models.Entity
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        [ForeignKey("Car")]
        public int CarID { get; set; }
        [ForeignKey("Garage")]
        public int GarageID { get; set; }
        [ForeignKey("Service")]
        public int ServiceID { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeUpdate { get; set; }
        public string Status { get; set; }
        public virtual Service? Service { get; set; }
        public Car Car { get; set; }
        public Garage Garage { get; set; }
    }
}
