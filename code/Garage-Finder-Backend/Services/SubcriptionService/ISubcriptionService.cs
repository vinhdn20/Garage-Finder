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
        public string GetLinkPay(int subscriptionId);
        public void Vnpay_ipn();
    }
}
