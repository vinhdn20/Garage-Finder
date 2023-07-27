using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class RoomChat
    {
        [Key]
        public int RoomID { get; set; }

        [ForeignKey("Users")]
        public int UserID { get; set; }
        [ForeignKey("Garage")]
        public int GarageID { get; set;}

        public Garage Garage { get; set; }
        public Users Users { get; set; }

        public ICollection<StaffMessage> StaffMessages { get; set;}
        public ICollection<Message> Message { get; set; }
    }
}
