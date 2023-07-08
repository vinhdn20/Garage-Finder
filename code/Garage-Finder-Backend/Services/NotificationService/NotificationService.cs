using AutoMapper;
using DataAccess.DTO;
using DataAccess.DTO.Orders.ResponseDTO;
using GFData.Models.Entity;
using Microsoft.AspNetCore.SignalR;
using Repositories.Interfaces;
using Services.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.NotificationService
{
    public class NotificationService : INotificationService
    {

        private readonly IHubContext<UserGFHub> _hubContext;
        private readonly INotifcationRepository _notifcationRepository;
        private readonly IGarageRepository _garageRepository;
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;
        public NotificationService(IHubContext<UserGFHub> hubContext, INotifcationRepository notifcationRepository,
            IGarageRepository garageRepository, IUsersRepository usersRepository)
        {
            _hubContext = hubContext;
            _notifcationRepository = notifcationRepository;
            _garageRepository = garageRepository;
            _userRepository = usersRepository;
        }

        public void SendNotificationToStaff(OrderDetailDTO orderDetail)
        {
            string message = GetStaffMessage(orderDetail.Status, orderDetail.Name);
            StaffNotification notification = new StaffNotification()
            {
                DateTime = DateTime.UtcNow,
                IsRead = false,
                Content = message
            };
            _notifcationRepository.AddStaffNotification(notification);
            NotificationDTO notificationDTO = _mapper.Map<NotificationDTO>(notification);
            _hubContext.Clients.Group($"Garage-{orderDetail.GarageID}").SendAsync("SendMessage", notificationDTO);
        }

        public void SendNotificatioToUser(OrderDetailDTO orderDetail, int userId)
        {
            string message = GetUserMessage(orderDetail.Status, orderDetail.GarageID);
            Notification notification = new Notification()
            {
                DateTime = DateTime.UtcNow,
                IsRead = false,
                Content = message
            }; 
            NotificationDTO notificationDTO = _mapper.Map<NotificationDTO>(notification);
            _hubContext.Clients.User(userId.ToString()).SendAsync("SendMessage", notificationDTO);
        }

        private string GetUserMessage(string status, int garageId)
        {
            string message = "";
            if (status.Equals(Constants.STATUS_ORDER_CONFIRMED))
            {
                var garage = _garageRepository.GetGaragesByID(garageId);
                message = $"Garage {garage.GarageName} đã xác nhận đơn của bạn";
            }
            else if(status.Equals(Constants.STATUS_ORDER_CANCELED))
            {

                var garage = _garageRepository.GetGaragesByID(garageId);
                message = $"Garage {garage.GarageName} đã hủy đơn của bạn";
            }
            else if (status.Equals(Constants.STATUS_ORDER_REJECT))
            {

                var garage = _garageRepository.GetGaragesByID(garageId);
                message = $"Garage {garage.GarageName} đã từ chối đơn của bạn";
            }
            else if (status.Equals(Constants.STATUS_ORDER_DONE))
            {

                var garage = _garageRepository.GetGaragesByID(garageId);
                message = $"Garage {garage.GarageName} đã hoàn thành đơn của bạn";
            }
            return message;
        }

        private string GetStaffMessage(string status, string userName)
        {
            string message = "";
            if (status.Equals(Constants.STATUS_ORDER_OPEN))
            {
                message = $"Có đơn mới từ {userName}";
            }
            else if (status.Equals(Constants.STATUS_ORDER_CANCELED))
            {
                message = $"Đơn của {userName} đã bị hủy";
            }
            return message;
        }
    }
}
