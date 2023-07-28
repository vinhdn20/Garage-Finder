using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Chat
{
    public class SendChatToUserByGarage
    {
        public string Content { get; set; }
        public int UserID { get; set; }
        public int FromGarageId { get; set; }
    }
}
