using DataAccess.DAO;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        public Invoices AddInVoices(Invoices invoices)
        {
            return InvoicesDAO.Instance.Add(invoices);
        }

        public Invoices GetInvoicesById(int id)
        {
            return InvoicesDAO.Instance.Get(id);
        }

        public void UpdateInvoices(Invoices invoices)
        {
            InvoicesDAO.Instance.Update(invoices);
        }

        public List<Subscribe> GetAllSubscribe()
        {
            return SubcriptionDAO.Instance.GetAll();
        }

        public Subscribe GetById(int id)
        {
            return SubcriptionDAO.Instance.GetSubscribeById(id);
        }
    }
}
