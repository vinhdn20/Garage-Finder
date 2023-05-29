using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFData.Models.Entity
{
    public class Service
    {
        [Key]
        public int ServiceID { get; set; }
        [ForeignKey("Garage")]
        public int GarageID { get; set; }
        public string NameService { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public double Cost { get; set; }
        public string Note { get; set; }
        public virtual Categorys? Category { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
