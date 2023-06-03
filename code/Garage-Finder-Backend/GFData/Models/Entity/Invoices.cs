using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class Invoices
    {
        [Key]
        public int InvoicesID { get; set; }
        [ForeignKey("Subscribe")]
        public int SubscribeID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? Note { get; set; }

        public Subscribe Subscribe { get; set; }
        public Users Users { get; set; }
    }
}
