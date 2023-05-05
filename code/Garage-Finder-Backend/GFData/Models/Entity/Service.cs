using System.ComponentModel.DataAnnotations;

namespace GFData.Models.Entity
{
    public class Service
    {
        [Key]
        public int ServiceID { get; set; }
        public int GarageID { get; set; }
        public string NameService { get; set; }
        public float Cost { get; set; }
        public string Note { get; set; }
        public virtual Category? Category { get; set; }
    }
}
