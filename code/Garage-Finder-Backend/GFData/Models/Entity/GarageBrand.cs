using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class GarageBrand
    {
        [Key]
        public int BrID { get; set; }
        [ForeignKey("Brand")]
        public int BrandID { get; set; }
        [ForeignKey("Garage")]
        public int GarageID { get; set; }
        public Garage Garage { get; set; }
        public Brand Brand { get; set; }
    }
}
