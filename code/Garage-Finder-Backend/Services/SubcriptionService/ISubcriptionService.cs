using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SubcriptionService
{
    public interface ISubcriptionService
    {
        public void AddInvoice();
        public string GetLinkPay(int userId, int garageId, int subscriptionId, string ipAddress);
        public void Vnpay_ipn();
    }
}
