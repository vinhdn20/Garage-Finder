using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class SubscribeDTO
    {
        public int SubscribeID { get; set; }
        public string Content { get; set; }
        public double Price { get; set; }
        public string Period { get; set; }
    }
}
