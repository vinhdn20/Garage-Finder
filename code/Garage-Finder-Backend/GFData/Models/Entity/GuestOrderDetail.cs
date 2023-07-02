using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class GuestOrderDetail
    {
        public int GuestOrderDetailId { get; set; }
        [ForeignKey("GuestOrder")]
        public int GuestOrderID { get; set; }
        [ForeignKey("CategoryGarage")]
        public int CategoryGarageID { get; set; }

        public GuestOrder Orders { get; set; }
        public virtual CategoryGarage? CategoryGarage { get; set; }
    }
}
