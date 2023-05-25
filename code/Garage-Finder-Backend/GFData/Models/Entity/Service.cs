using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFData.Models.Entity
{
    public class Service
    {
        [Key]
        public int ServiceID { get; set; }
        public string NameService { get; set; }
        public double Cost { get; set; }
        public string Note { get; set; }


        //[ForeignKey("GarageID")]
        public int GarageID { get; set; }
        public Garage Garage { get; set; }

        //[ForeignKey("CategoryID")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
