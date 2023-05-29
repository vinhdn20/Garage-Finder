using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFData.Models.Entity
{
    public class Car
    {
        [Key]
        public int CarID { get; set; }
        public int UserID { get; set; }
        public string LicensePlates { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }

        public Users User { get; set; }
    }
}
