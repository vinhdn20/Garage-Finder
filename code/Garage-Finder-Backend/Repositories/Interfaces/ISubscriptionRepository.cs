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
    }
}
