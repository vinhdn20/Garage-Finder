using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFData.Models.Entity
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }
        public string NameService { get; set; }
        public double Cost { get; set; }
        public string Note { get; set; }

        //[ForeignKey("OrderID")]
        public int OrderID { get; set; }
        public Orders Orders { get; set; }

        //[ForeignKey("ServiceID")]
        public int ServiceID { get; set; }
        public Service Service { get; set; }

    }
}
