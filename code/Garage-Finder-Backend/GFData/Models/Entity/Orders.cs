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
        [ForeignKey("CategoryGarage")]
        public int CategoryGarageID { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeUpdate { get; set; }
        public DateTime? TimeAppointment { get; set; }
        public string? Status { get; set; }
        public string? Content { get; set; }

        public virtual CategoryGarage? CategoryGarage { get; set; }
        public Car Car { get; set; }
        public Garage Garage { get; set; }

        public ICollection<ImageOrders> ImageOrders { get; set; }
        public ICollection<FileOrders> FileOrders { get; set; }
    }
}
