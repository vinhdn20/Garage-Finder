using System.ComponentModel.DataAnnotations;

namespace GFData.Models.Entity
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public int CarID { get; set; }
        public int GarageID { get; set; }
        public string TimeCreate { get; set; }
        public string TimeUpdate { get; set; }
        public string Status { get; set; }
        public virtual Users User { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
    }
}
