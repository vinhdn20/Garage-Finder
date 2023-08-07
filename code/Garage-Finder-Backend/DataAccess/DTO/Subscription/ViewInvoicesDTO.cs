using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Subscription
{
    public class ViewInvoicesDTO
    {
        public int InvoicesID { get; set; }
        public string Name { get; set; }
        public int GarageID { get; set; }
        public string GarageName { get; set; }
        public string GaragePhone { get; set; }
        public string GarageEmail { get; set; }
        public string GarageAddress { get; set; }
        public int SubscribeID { get; set; }
        public string Status { get; set; }

    }
}
