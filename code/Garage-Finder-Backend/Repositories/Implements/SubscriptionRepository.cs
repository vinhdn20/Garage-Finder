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
