using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GFData.Models.Entity
{
    [Index(nameof(GFOrderID), IsUnique = true)]
    public class GuestOrder
    {

        [Key]
        public int GuestOrderID { get; set; }
        public int GFOrderID { get; set; }
        [ForeignKey("Garage")]
        public int GarageID { get; set; }
        [ForeignKey("CategoryGarage")]
        public int CategoryGarageID { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeUpdate { get; set; }
        public DateTime? TimeAppointment { get; set; }
        public string? Status { get; set; }
        public string? Content { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        [ForeignKey("Brand")]
        public int? BrandCarID { get; set; }
        public string? TypeCar { get; set; }
        public string? LicensePlates { get; set; }
        public virtual CategoryGarage? CategoryGarage { get; set; }

        public Garage Garage { get; set; }
        public Brand Brand { get; set; }
        public ICollection<ImageGuestOrder> ImageOrders { get; set; }
        public ICollection<FileGuestOrders> FileOrders { get; set; }
    }
}
