using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class ImageCar
    {
        [Key]
        public int ImageId { get; set; }
        [ForeignKey("Car")]
        public int CarID { get; set; }
        public string ImageLink { get; set; }

        public Car Car { get; set; }
    }
}
