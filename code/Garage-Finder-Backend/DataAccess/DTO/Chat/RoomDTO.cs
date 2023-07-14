using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Chat
{
    public class RoomDTO
    {
        public int RoomID { get; set; }
        public int ToId { get; set; }
        public string Name { get; set; }
        public string LinkImage { get; set; }
        public DateTime DateTime { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
    }
}
