using DataAccess.DAO;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements.NotificationRepository
{
    public class NotifcationRepository : INotifcationRepository
    {
        public StaffNotification AddStaffNotification(StaffNotification notification)
        {
            return StaffNotificationDAO.Instance.AddNotification(notification);
        }

        public Notification AddUserNotification(Notification notification)
        {
            return NotificationDAO.Instance.AddNotification(notification);
        }

        public List<StaffNotification> GetNotificationsByStaffId(int id)
        {
            return StaffNotificationDAO.Instance.GetNotificationsByUserId(id).ToList();
        }

        public List<Notification> GetNotificationsByUserId(int id)
        {
            return NotificationDAO.Instance.GetNotificationsByUserId(id).ToList();
        }

        public void UpdateNotification(Notification notification)
        {
            NotificationDAO.Instance.Update(notification);
        }

        public void UpdateNotification(StaffNotification notification)
        {
            StaffNotificationDAO.Instance.Update(notification);
        }
    }
}
