using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class MessageToUser
    {
        [Key]
        public int MessageID { get; set; }
        [ForeignKey(nameof(Users))]
        public int SenderUserID { get; set; }
        [ForeignKey(nameof(Users))]
        public int ReceiverUserID { get; set; }
        public DateTime DateTime { get; set; }
        public string Content { get; set; }

        public Users Sender { get; set; }
        public Users Receiver { get; set; }
    }
}
