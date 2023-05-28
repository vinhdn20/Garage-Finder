using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFData.Models.Entity
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public int CarID { get; set; }
        public int GarageID { get; set; }
        public int ServiceID { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeUpdate { get; set; }
        public string Status { get; set; }
        public virtual Category? Category { get; set; }
    }
}
