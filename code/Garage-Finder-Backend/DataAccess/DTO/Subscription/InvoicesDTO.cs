using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Subscription
{
    public class InvoicesDTO
    {
        public int InvoicesID { get; set; }
        public int Name { get; set; }
        public int Period { get; set; }
        public double Price { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
