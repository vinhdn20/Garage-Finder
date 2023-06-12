using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class ImageGarageDTO
    {
        public int ImageID { get; set; }
        public int GarageID { get; set; }
        public string ImageLink { get; set; }
    }
}
