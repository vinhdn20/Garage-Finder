using DataAccess.DTO.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ChatService
{
    public interface IChatService
    {
        void SendToGarage(int userId, SendChatToGarage sendChat);
        void SendToUser(int userId, string nameRole, SendChatToUserByGarage sendChat);
        List<RoomDTO> GetListRoomByUserId(int userId);
        List<RoomDTO> GetListRoomByGarageId(int userId, int garageId);
        List<ChatDTO> GetDetailMessage(int userId, string nameRole, int roomId);
    }
}
