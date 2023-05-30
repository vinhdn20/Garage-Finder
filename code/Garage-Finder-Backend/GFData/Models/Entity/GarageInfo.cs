using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFData.Models.Entity
{
    public class GarageInfo
    {
        [Key]
        public int InfoID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        [ForeignKey("Garage")]
        public int GarageID { get; set; }
        public Garage Garage { get; set; }
        public Users User { get; set; }
    }
}
