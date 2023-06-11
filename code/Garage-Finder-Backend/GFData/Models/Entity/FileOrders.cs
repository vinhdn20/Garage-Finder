using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class FileOrders
    {
        [Key]
        public int FileId { get; set; }
        [ForeignKey("Orders")]
        public int OrderID { get; set; }
        public string FileLink { get; set; }

        public Orders Orders { get; set; }
    }
}
