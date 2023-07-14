using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }
        [ForeignKey("Users")]
        public int UserID { get; set; }
        [ForeignKey("RoomChat")]
        public int RoomID { get; set; }
        [ForeignKey("Garage")]
        public int GarageID { get; set; }
        public DateTime DateTime { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }

        public Users Users { get; set; }
        public Garage Garage { get; set; }
        public RoomChat RoomChat { get; set; }

    }
}
