using GFData.Data;
using GFData.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class ServiceDAO
    {
        private static ServiceDAO instance = null;
        private static readonly object iLock = new object();
        public ServiceDAO()
        {

        }

        public static ServiceDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new ServiceDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Service> GetServices()
        {
            var listServices = new List<Service>();
            try
            {
                using (var context = new GFDbContext())
                {
                    listServices = context.Service.Include(p => p.Category).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listServices;
        }

        public Service FindServiceById(int serviceID)
        {
            var p = new Service();
            try
            {
                using (var context = new GFDbContext())
                {
                    p = context.Service.Include(p => p.Category).SingleOrDefault(x => x.ServiceID == serviceID);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;
        }

        public void SaveService(Service p)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Service.Add(p);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateService(Service p)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Entry<Service>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteService(int id)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    var pDelete = context.Service.SingleOrDefault(x => x.ServiceID == id);
                    context.Service.Remove(pDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
