using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class NotificationDAO
    {
        private static NotificationDAO instance = null;
        private static readonly object iLock = new object();
        public NotificationDAO()
        {

        }

        public static NotificationDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new NotificationDAO();
                    }
                    return instance;
                }
            }
        }

        public Notification AddNotification(Notification notification)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Notification.Add(notification);
                    return notification;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.InnerException.Message);
            }
        }

        public List<Notification> GetNotificationsByUserId(int id)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    var notification = context.Notification.Where(x => x.UserID == id).ToList();
                    return notification;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.InnerException.Message);
            }
        }
    }
}
