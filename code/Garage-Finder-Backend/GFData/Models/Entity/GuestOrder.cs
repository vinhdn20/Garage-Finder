using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class GuestOrder
    {

        [Key]
        public int GuestOrderID { get; set; }
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
        public Garage Garage { get; set; }

        public ICollection<ImageGuestOrder> ImageOrders { get; set; }
        public ICollection<FileGuestOrders> FileOrders { get; set; }
    }
}
