using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class StaffNotificationDAO
    {
        private static StaffNotificationDAO instance = null;
        private static readonly object iLock = new object();
        public StaffNotificationDAO()
        {

        }

        public static StaffNotificationDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new StaffNotificationDAO();
                    }
                    return instance;
                }
            }
        }

        public StaffNotification AddNotification(StaffNotification notification)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.StaffNotification.Add(notification);
                    context.SaveChanges();
                    return notification;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.InnerException.Message);
            }
        }

        public List<StaffNotification> GetNotificationsByUserId(int id)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    var notification = context.StaffNotification.Where(x => x.StaffId == id).ToList();
                    return notification;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.InnerException.Message);
            }
        }
        public void Update(StaffNotification notification)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.StaffNotification.Update(notification);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.InnerException.Message);
            }
        }
    }
}
