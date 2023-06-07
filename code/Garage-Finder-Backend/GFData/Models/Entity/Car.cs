using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace GFData.Models.Entity
{
    [Index(nameof(LicensePlates), IsUnique = true)]
    public class Car
    {
        [Key]
        public int CarID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public string LicensePlates { get; set; }
        [ForeignKey("Brand")]
        public int BrandID { get; set; }
        public string? Color { get; set; }
        public string? TypeCar { get; set; }
        public string? LinkImages { get; set; }

        public Users User { get; set; }
        public Brand Brand { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
