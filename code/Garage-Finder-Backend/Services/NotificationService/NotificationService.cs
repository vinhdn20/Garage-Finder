﻿using AutoMapper;
using DataAccess.DTO;
using DataAccess.DTO.Orders;
using DataAccess.DTO.Orders.ResponseDTO;
using GFData.Models.Entity;
using Mailjet.Client.Resources;
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

        //private readonly IHubContext<UserGFHub> _hubContext;
        private readonly INotifcationRepository _notifcationRepository;
        private readonly IGarageRepository _garageRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IUsersRepository _userRepository;
        private readonly ICarRepository _carRepository;
        private readonly WebsocketSend _webSocketHandler;
        private readonly IMapper _mapper;
        public NotificationService(INotifcationRepository notifcationRepository,
            IGarageRepository garageRepository, IUsersRepository usersRepository, IMapper mapper,
            IStaffRepository staffRepository, WebsocketSend gFWebSocket, ICarRepository carRepository)
        {
            _notifcationRepository = notifcationRepository;
            _garageRepository = garageRepository;
            _userRepository = usersRepository;
            _mapper = mapper;
            _staffRepository = staffRepository;
            _webSocketHandler = gFWebSocket;
            _carRepository = carRepository;
        }

        public void SendNotificationToStaff(OrdersDTO order, int userId)
        {
            var user = _userRepository.GetUserByID(userId);
            string message = GetStaffMessage(order.Status, user.Name);
            StaffNotification notification = new StaffNotification()
            {
                DateTime = DateTime.UtcNow,
                IsRead = false,
                Content = message
            };
            var staffs = _staffRepository.GetByGarageId(order.GarageID);
            foreach (var staff in staffs)
            {
                notification.StaffNotificationID = 0;
                notification.StaffId = staff.StaffId;
                _notifcationRepository.AddStaffNotification(notification);
            }
            var id = _garageRepository.GetGaragesByID(order.GarageID).UserID;
            Notification notiUser = new Notification()
            {
                DateTime = DateTime.UtcNow,
                IsRead = false,
                Content = message,
                UserID = id
            };
            _notifcationRepository.AddUserNotification(notiUser);
            NotificationDTO notificationDTO = _mapper.Map<NotificationDTO>(notification);
            //_hubContext.Clients.Group($"Garage-{order.GarageID}").SendAsync("Notify", notificationDTO);

            _webSocketHandler.SendToGroup($"Garage-{order.GarageID}", "Notify", notificationDTO);
        }

        public void SendNotificatioToUser(OrdersDTO order, int userId)
        {
            string message = GetUserMessage(order.Status, order.GarageID);
            var id = _carRepository.GetCarById(order.CarID).UserID;
            Notification notification = new Notification()
            {
                DateTime = DateTime.UtcNow,
                IsRead = false,
                Content = message,
                UserID = id
            }; 
            NotificationDTO notificationDTO = _mapper.Map<NotificationDTO>(notification);
            _notifcationRepository.AddUserNotification(notification);
            //_hubContext.Clients.User(userId.ToString()).SendAsync("Notify", notificationDTO);

            _webSocketHandler.SendAsync(id.ToString(), "Notify", notificationDTO);
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
            return message.convertToUTF8();
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
            return message.convertToUTF8();
        }

        public void SendNotificationToStaff(GuestOrderDTO orderDetail)
        {
            string message = GetStaffMessage(orderDetail.Status, orderDetail.Name);
            StaffNotification notification = new StaffNotification()
            {
                DateTime = DateTime.UtcNow,
                IsRead = false,
                Content = message
            };
            var staffs = _staffRepository.GetByGarageId(orderDetail.GarageID);
            foreach (var staff in staffs)
            {
                notification.StaffId = staff.StaffId;
                _notifcationRepository.AddStaffNotification(notification);
            }
            
            NotificationDTO notificationDTO = _mapper.Map<NotificationDTO>(notification);
            //_hubContext.Clients.Group($"Garage-{orderDetail.GarageID}").SendAsync("Notify", notificationDTO);
            _webSocketHandler.SendToGroup($"Garage-{orderDetail.GarageID}", "Notify", notificationDTO);
        }

        public List<NotificationDTO> GetNotification(int userId, string roleName)
        {
            List<NotificationDTO> notify = new List<NotificationDTO>();
            if (roleName.Equals(Constants.ROLE_USER))
            {
                var userNotify = _notifcationRepository.GetNotificationsByUserId(userId);
                //userNotify.ForEach(x => notify.Add(_mapper.Map<NotificationDTO>(x)));
                foreach (var noti in userNotify)
                {
                    noti.DateTime = noti.DateTime.AddHours(7);
                    var addNoti = _mapper.Map<NotificationDTO>(noti);
                    notify.Add(addNoti);
                }
            }
            else if (roleName.Equals(Constants.ROLE_STAFF))
            {
                var userNotify = _notifcationRepository.GetNotificationsByStaffId(userId);
                //userNotify.ForEach(x => notify.Add(_mapper.Map<NotificationDTO>(x)));
                foreach (var noti in userNotify)
                {
                    noti.DateTime = noti.DateTime.AddHours(7);
                    var addNoti = _mapper.Map<NotificationDTO>(noti);
                    notify.Add(addNoti);
                }
            }
            return notify.OrderByDescending(x => x.DateTime).ToList();
        }

        public void ReadAllNotification(int userId, string roleName)
        {
            List<NotificationDTO> notify = new List<NotificationDTO>();
            if (roleName.Equals(Constants.ROLE_USER))
            {
                var userNotify = _notifcationRepository.GetNotificationsByUserId(userId).Where(x => x.IsRead == false);
                foreach (var noti in userNotify)
                {
                    noti.IsRead = true;
                    _notifcationRepository.UpdateNotification(noti);
                }
            }
            else if (roleName.Equals(Constants.ROLE_STAFF))
            {
                var userNotify = _notifcationRepository.GetNotificationsByStaffId(userId);
                foreach (var noti in userNotify)
                {
                    noti.IsRead = true;
                    _notifcationRepository.UpdateNotification(noti);
                }
            }
        }
    }
}
