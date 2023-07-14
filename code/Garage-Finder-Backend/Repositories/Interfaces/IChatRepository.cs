using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IChatRepository
    {
        List<RoomChat> GetRoomByStaff(int staffId);
        List<RoomChat> GetRoomByUser(int userId);
        List<StaffMessage> GetStaffMessages(int roomId);
        List<Message> GetMessages(int roomId);
        void StaffSendMessage(StaffMessage message, int userId);
        void UserSendMessage(Message message, int garageId);
        RoomChat CreateRoomChat();
    }
}
