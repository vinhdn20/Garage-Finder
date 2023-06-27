using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Orders
{
    public class ImageOrdersDTO
    {
        public int ImageID { get; set; }
        public int OrderID { get; set; }
        public string ImageLink { get; set; }
    }
}
