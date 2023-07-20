using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class SubcriptionDAO
    {
        private static SubcriptionDAO instance = null;
        private static readonly object iLock = new object();
        public SubcriptionDAO()
        {

        }

        public static SubcriptionDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new SubcriptionDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Subscribe> GetAll()
        {
            List<Subscribe> subscribes = new List<Subscribe>();
            try
            {
                using (var context = new GFDbContext())
                {
                    subscribes = context.Subscribes.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return subscribes;
        }

        public void Add(Subscribe sub)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Subscribes.Add(sub);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(Subscribe sub)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Subscribes.Update(sub);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(Subscribe sub)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Subscribes.Remove(sub);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Subscribe GetSubscribeById(int id)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    var sub = context.Subscribes.FirstOrDefault(x => x.SubscribeID == id);
                    if(sub != null)
                    {
                        return sub;
                    }
                    else
                    {
                        throw new Exception("Wrong id");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
