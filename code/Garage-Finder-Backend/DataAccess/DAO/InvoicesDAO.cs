using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class InvoicesDAO
    {
        private static InvoicesDAO instance = null;
        private static readonly object iLock = new object();
        public InvoicesDAO()
        {

        }
        public static InvoicesDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new InvoicesDAO();
                    }
                    return instance;
                }
            }
        }

        public Invoices Add(Invoices invoice)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Invoices.Add(invoice);
                    context.SaveChanges();
                    return invoice;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "|" + e.InnerException);
            }
        }

        public Invoices Get(int id)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    var invoice = context.Invoices.First(x => x.InvoicesID == id);
                    return invoice;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "|" + e.InnerException);
            }
        }

        public List<Invoices> GetByGarageId(int garageId)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    var invoice = context.Invoices.Where(x => x.GarageID == garageId).ToList();
                    return invoice;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "|" + e.InnerException);
            }
        }

        public void Update(Invoices invoice)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Invoices.Update(invoice);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "|" + e.InnerException);
            }
        }
    }
}
