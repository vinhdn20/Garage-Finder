using DataAccess.DTO;
using DataAccess.DTO.Chat;
using DataAccess.DTO.Token;
using GFData.Models.Entity;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Repositories.Interfaces;
using Services.ChatService;
using Services.NotificationService;
using SignalRSwaggerGen.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.WebSocket
{
    [Authorize]
    [SignalRHub]
    public class UserGFHub : Hub
    {
        private readonly IGarageRepository _garageRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IChatService _chatService;
        private readonly INotificationService _notificationService;

        public UserGFHub(IGarageRepository garageRepository, IStaffRepository staffRepository,
            INotificationService notificationService, IChatService chatService)
        {
            _garageRepository = garageRepository;
            _staffRepository = staffRepository;
            _notificationService = notificationService;
            _chatService = chatService;
        }
        public override async Task OnConnectedAsync()
        {
            var user = GetTokenInfor();
            if (user.RoleName.Equals(Constants.ROLE_STAFF))
            {
                var staff = _staffRepository.GetById(user.UserID);
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Garage-{staff.GarageID}");
            }
            var garages = _garageRepository.GetGarageByUser(user.UserID);
            foreach (var garage in garages)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Garage-{garage.GarageID}");
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, $"GF-User");
            await base.OnConnectedAsync();
        }

        [SignalRMethod]
        public void ReadAllNotification()
        {
            var user = GetTokenInfor();
            _notificationService.ReadAllNotification(user.UserID, user.RoleName);
        }

        [SignalRMethod]
        public List<NotificationDTO> GetAllNotification()
        {
            var user = GetTokenInfor();
            return _notificationService.GetNotification(user.UserID, user.RoleName);
        }

        public List<RoomDTO> GetListRoom()
        {
            var user = GetTokenInfor();
            return _chatService.GetListRoom(user.UserID, user.RoleName);
        }

        public List<ChatDTO> GetDetailRoom(int roomId)
        {
            var user = GetTokenInfor();
            return _chatService.GetDetailMessage(user.UserID, user.RoleName, roomId);
        }

        public void SendMessgeToUser(SendChat sendChat)
        {
            var user = GetTokenInfor();
            _chatService.SendToUser(user.UserID, sendChat);
        }

        public void SendMessageToGarage(SendChat sendChat)
        {
            var user = GetTokenInfor();
            _chatService.SendToGarage(user.UserID, sendChat);
        }

        private TokenInfor GetTokenInfor()
        {
            var jsonUser = Context.User.FindFirstValue("user");
            var user = JsonConvert.DeserializeObject<TokenInfor>(jsonUser);
            return user;
        }
    }
}
