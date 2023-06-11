using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class ImageCarDTO
    {
        public int ImageId { get; set; }
        public int CarID { get; set; }
        public string ImageLink { get; set; }
    }
}
