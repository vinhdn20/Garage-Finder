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
        [ForeignKey("CategoryGarage")]
        public int CategoryGarageID { get; set; }
        public string? Note { get; set; }
        public string? Cost { get; set; }
        public virtual CategoryGarage? CategoryGarage { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
