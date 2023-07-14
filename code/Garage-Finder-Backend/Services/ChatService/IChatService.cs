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
        void SendToGarage(int userId, SendChat sendChat);
        void SendToUser(int fromStaffId, SendChat sendChat);
        List<RoomDTO> GetListRoom(int userId, string roleName);
        List<ChatDTO> GetDetailMessage(int userId, string nameRole, int roomId);
    }
}
