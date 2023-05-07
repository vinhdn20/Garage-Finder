using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpriresDate { get; set; }
    }
}
