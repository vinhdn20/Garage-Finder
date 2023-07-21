using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ISubscriptionRepository
    {
        List<Subscribe> GetAllSubscribe();
        Subscribe GetById(int id);
        Invoices AddInVoices(Invoices invoices);
        Invoices GetInvoicesById(int id);
        List<Invoices> GetInvoicesByGarageId(int garageId);
        void UpdateInvoices(Invoices invoices);
        void AddSubcribe(Subscribe subscribe);
        void UpdateSubribe(Subscribe subscribe);
    }
}
