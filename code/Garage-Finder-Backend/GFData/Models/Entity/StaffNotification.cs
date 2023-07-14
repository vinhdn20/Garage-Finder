using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class StaffNotification
    {
        [Key]
        public int StaffNotificationID { get; set; }
        public DateTime DateTime { get; set; }
        public string Content { get; set; }

        [ForeignKey("Staff")]
        public int StaffId { get; set; }
        public bool IsRead { get; set; }

        public Staff Staff { get; set; }
    }
}
