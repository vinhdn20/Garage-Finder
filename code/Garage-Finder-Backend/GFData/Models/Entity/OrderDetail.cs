using System.ComponentModel.DataAnnotations;

namespace GFData.Models.Entity
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ServiceID { get; set; }
        public string NameService { get; set; }
        public double Cost { get; set; }
        public string Note { get; set; }
        public virtual Service Service { get; set; }

    }
}
