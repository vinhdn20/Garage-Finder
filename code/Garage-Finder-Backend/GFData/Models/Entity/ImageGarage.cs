using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class ImageGarage
    {
        [Key]
        public int ImageID { get; set; }
        [ForeignKey("Garage")]
        public int GarageID { get; set; }
        public string ImageLink { get; set; }

        public Garage Garage { get; set; }
    }
}
