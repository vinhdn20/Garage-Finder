using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Subscription
{
    public class SubscribeDTO
    {
        public int SubscribeID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public double Price { get; set; }
        public int Period { get; set; }
        public int Status { get; set; }
    }
}
