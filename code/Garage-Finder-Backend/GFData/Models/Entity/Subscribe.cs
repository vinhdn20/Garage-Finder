using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class Subscribe
    {
        [Key]
        public int SubscribeID { get; set; }
        public string Content { get; set; }
        public double Price { get; set; }
        public string Period { get; set; }

        public ICollection<Invoices> Invoices { get; set; }
    }
}
