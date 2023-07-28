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
using System.Reflection.Metadata.Ecma335;
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

        public List<RoomDTO> GetListRoomByUserId()
        {
            var user = GetTokenInfor();
            return _chatService.GetListRoomByUserId(user.UserID);
        }

        public List<RoomDTO> GetListRoomByGarageId(int garageId)
        {
            var user = GetTokenInfor();
            return _chatService.GetListRoomByGarageId(user.UserID,garageId);
        }

        public List<ChatDTO> GetDetailRoom(int roomId)
        {
            var user = GetTokenInfor();
            return _chatService.GetDetailMessage(user.UserID, user.RoleName, roomId);
        }

        public void GarageSendMessgeToUser(SendChatToUserByGarage sendChat)
        {
            var user = GetTokenInfor();
            _chatService.SendToUser(user.UserID, user.RoleName, sendChat);
        }

        public void UserSendMessageToGarage(SendChatToGarage sendChat)
        {
            var user = GetTokenInfor();
            _chatService.SendToGarage(user.UserID, sendChat);
        }

        public void SendMessageToUser(SendChatToUser sendChat)
        {
            var user = GetTokenInfor();
            _chatService.SendMessageToUser(user.UserID, sendChat.ToUserId, sendChat.Content);
        }

        public List<ChatDTO> GetMessageWithUser(int userId)
        {
            var user = GetTokenInfor();
            return _chatService.GetMessageWithUser(user.UserID, userId);
        }

        private TokenInfor GetTokenInfor()
        {
            var jsonUser = Context.User.FindFirstValue("user");
            var user = JsonConvert.DeserializeObject<TokenInfor>(jsonUser);
            return user;
        }
    }
}
