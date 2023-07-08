using DataAccess.DTO.Orders.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.NotificationService
{
    public interface INotificationService
    {
        void SendNotificatioToUser(OrderDetailDTO orderDetail, int userId);
        void SendNotificationToStaff(OrderDetailDTO orderDetail);
    }
}
