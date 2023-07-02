using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        [ForeignKey("Orders")]
        public int OrderId { get; set; }
        [ForeignKey("CategoryGarage")]
        public int CategoryGarageID { get; set; }

        public Orders Orders { get; set; }
        public virtual CategoryGarage? CategoryGarage { get; set; }
    }
}
