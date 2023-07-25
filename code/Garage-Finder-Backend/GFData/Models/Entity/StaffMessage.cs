using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class StaffMessage
    {
        [Key]
        public int StaffMessageID { get; set; }
        [ForeignKey("Staff")]
        public int StaffId { get; set; }
        [ForeignKey("RoomChat")]
        public int RoomID { get; set; }
        public DateTime DateTime { get; set; }
        public string Content { get; set; }
        [ForeignKey("Users")]
        public int UserID { get; set; }
        public bool IsRead { get; set; }

        public Users User { get; set; }
        public RoomChat RoomChat { get; set; }
        public Staff Staff { get; set; }
    }
}
