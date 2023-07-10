using DataAccess.DTO;
using DataAccess.DTO.Orders;

namespace Services.NotificationService
{
    public interface INotificationService
    {
        void SendNotificatioToUser(OrdersDTO orderDetail, int userId);
        void SendNotificationToStaff(OrdersDTO orderDetail,  int userId);
        void SendNotificationToStaff(GuestOrderDTO orderDetail);
        List<NotificationDTO> GetNotification(int userId, string roleName);
        void ReadAllNotification(int userId, string roleName);
    }
}
