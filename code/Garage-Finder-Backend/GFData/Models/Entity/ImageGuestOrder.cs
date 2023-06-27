using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class ImageGuestOrder
    {
        [Key]
        public int ImageID { get; set; }
        [ForeignKey("GuestOrder")]
        public int GuestOrderID { get; set; }
        public string ImageLink { get; set; }

        public GuestOrder Orders { get; set; }
    }
}
