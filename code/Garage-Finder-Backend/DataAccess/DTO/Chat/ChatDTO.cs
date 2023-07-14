using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Chat
{
    public class ChatDTO
    {
        public DateTime DateTime { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public bool IsSendByUser { get; set; }
    }
}
