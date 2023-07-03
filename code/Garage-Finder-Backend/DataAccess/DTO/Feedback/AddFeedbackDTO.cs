using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Feedback
{
    public class AddFeedbackDTO
    {
        public int GFOrderID { get; set; }
        public int Star { get; set; }
        public string Content { get; set; }
    }
}
