using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Subscription
{
    public class AddSubcribeDTO
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public double Price { get; set; }
        public int Period { get; set; }
    }
}
