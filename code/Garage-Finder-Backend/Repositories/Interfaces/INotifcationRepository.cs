using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface INotifcationRepository
    {
        Notification AddUserNotification(Notification notification);
        StaffNotification AddStaffNotification(StaffNotification notification);
        List<Notification> GetNotificationsByUserId(int id);
        List<StaffNotification> GetNotificationsByStaffId(int id);
        void UpdateNotification(Notification notification);
        void UpdateNotification(StaffNotification notification);
    }
}
