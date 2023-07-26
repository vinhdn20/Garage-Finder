using DataAccess.DAO;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements.Chat
{
    public class ChatRepository : IChatRepository
    {
        public List<RoomChat> GetRoomByGarageId(int garageId)
        {
            var rooms = ChatDAO.Instance.GetRoomChatByGarage(garageId);
            return rooms;
        }

        public List<RoomChat> GetRoomByUser(int userId)
        {
            var rooms = ChatDAO.Instance.GetRoomChatByUser(userId);
            return rooms;
        }

        public List<StaffMessage> GetStaffMessages(int roomId)
        {
            var messages = ChatDAO.Instance.GetStaffMessagesByRoom(roomId);
            return messages;
        }

        public List<Message> GetMessages(int roomId)
        {
            var messages = ChatDAO.Instance.GetMessagesByRoom(roomId);
            return messages;
        }

        public void StaffSendMessage(StaffMessage message, int userId)
        {
            ChatDAO.Instance.StaffSendMessage(message);
        }

        public void UserSendMessage(Message message, int garageId)
        {
            ChatDAO.Instance.UserSendMessage(message);
        }

        public RoomChat CreateRoomChat(RoomChat room)
        {
            return ChatDAO.Instance.CreateRoomChat(room);
        }

        public RoomChat GetRoomById(int id)
        {
            return ChatDAO.Instance.GetRoomById(id);
        }
    }
}
