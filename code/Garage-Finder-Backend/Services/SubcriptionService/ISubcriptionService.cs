using DataAccess.DTO.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SubcriptionService
{
    public interface ISubcriptionService
    {
        public void AddInvoice(VNPayIPNDTO vNPay);
        public string GetLinkPay(int userId, int garageId, int subscriptionId, string ipAddress);
    }
}
