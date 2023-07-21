using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    [Index(nameof(Name), IsUnique = true)]
    public class Subscribe
    {
        [Key]
        public int SubscribeID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public double Price { get; set; }
        public int Period { get; set; }
        public string Status { get; set; }

        public ICollection<Invoices> Invoices { get; set; }
    }
}
