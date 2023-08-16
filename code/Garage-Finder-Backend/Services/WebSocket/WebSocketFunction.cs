using DataAccess.DTO.Chat;
using DataAccess.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Jwt.AccessToken;
using DataAccess.DTO.Token;
using Repositories.Interfaces;
using Services.ChatService;
using Services.NotificationService;
using Newtonsoft.Json;
using System.Net.WebSockets;

namespace Services.WebSocket
{
    public class WebSocketFunction
    {
        private readonly IChatService _chatService;
        private readonly INotificationService _notificationService;
        public WebSocketFunction(INotificationService notificationService, IChatService chatService)
        {
            _notificationService = notificationService;
            _chatService = chatService;
        }

        public void ReadAllNotification(TokenInfor user)
        {
            _notificationService.ReadAllNotification(user.UserID, user.RoleName);
        }

        public List<NotificationDTO> GetAllNotification(TokenInfor user)
        {
            return _notificationService.GetNotification(user.UserID, user.RoleName);
        }

        public List<RoomDTO> GetListRoomByUserId(TokenInfor user)
        {
            return _chatService.GetListRoomByUserId(user.UserID);
        }

        public List<RoomDTO> GetListRoomByGarageId(TokenInfor user, int garageId)
        {
            return _chatService.GetListRoomByGarageId(user.UserID, garageId);
        }

        public List<ChatDTO> GetDetailRoom(TokenInfor user, int roomId)
        {
            return _chatService.GetDetailMessage(user.UserID, user.RoleName, roomId);
        }

        public void GarageSendMessgeToUser(TokenInfor user, SendChatToUserByGarage sendChat)
        {
            _chatService.SendToUser(user.UserID, user.RoleName, sendChat);
        }

        public void UserSendMessageToGarage(TokenInfor user, SendChatToGarage sendChat)
        {
            _chatService.SendToGarage(user.UserID, sendChat);
        }
    }
}
