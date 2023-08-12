using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ChatDAO
    {
        private static ChatDAO instance = null;
        private static readonly object iLock = new object();
        public ChatDAO()
        {

        }

        public static ChatDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new ChatDAO();
                    }
                    return instance;
                }
            }
        }
        public RoomChat GetRoomById(int id)
        {
            RoomChat roomChats = new RoomChat();
            try
            {
                using (var context = new GFDbContext())
                {
                    roomChats = (from room in context.RoomChat
                                 where room.RoomID == id
                                 select new RoomChat()
                                 {
                                     RoomID = room.RoomID,
                                     GarageID = room.GarageID,
                                     UserID = room.UserID,
                                 }).FirstOrDefault();
                }
                return roomChats;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.InnerException.Message);
            }
        }

        public List<RoomChat> GetRoomChatByUser(int userId)
        {
            List<RoomChat> roomChats = new List<RoomChat>();
            try
            {
                using (var context = new GFDbContext())
                {
                    roomChats = (from room in context.RoomChat
                                 where room.UserID == userId
                                 select new RoomChat()
                                 {
                                     RoomID = room.RoomID,
                                     GarageID = room.GarageID,
                                     UserID = room.UserID,
                                 }).ToList();
                }
                return roomChats;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.InnerException.Message);
            }            
        }

        public List<RoomChat> GetRoomChatByGarage(int garageId)
        {
            List<RoomChat> roomChats = new List<RoomChat>();
            try
            {
                using (var context = new GFDbContext())
                {
                    roomChats = (from room in context.RoomChat
                                 where room.GarageID == garageId
                                 select new RoomChat()
                                 {
                                     RoomID = room.RoomID,
                                     GarageID = room.GarageID,
                                     UserID = room.UserID,
                                 }).ToList();
                }
                return roomChats;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.InnerException.Message);
            }
        }

        public List<StaffMessage> GetStaffMessagesByRoom(int roomId)
        {
            List<StaffMessage> messages = new List<StaffMessage>();
            try
            {
                using (var context = new GFDbContext())
                {
                    messages = (from message in context.StaffMessages
                                join room in context.RoomChat on message.RoomID equals roomId
                                where room.RoomID == roomId
                                select new StaffMessage()
                                {
                                    RoomID = room.RoomID,
                                    StaffMessageID = message.StaffMessageID,
                                    Content = message.Content,
                                    DateTime = message.DateTime,
                                    StaffId = message.StaffId,                                    
                                }).ToList();
                }
                return  messages;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.InnerException.Message);
            }
        }

        public List<Message> GetMessagesByRoom(int roomId)
        {
            List<Message> messages = new List<Message>();
            try
            {
                using (var context = new GFDbContext())
                {
                    messages = (from message in context.Message
                                join room in context.RoomChat on message.RoomID equals roomId
                                where room.RoomID == roomId
                                select new Message()
                                {
                                    RoomID = room.RoomID,
                                    MessageID = message.MessageID,
                                    Content = message.Content,
                                    DateTime = message.DateTime,
                                    UserID = message.UserID,
                                }).ToList();
                }
                return messages;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.InnerException.Message);
            }
        }

        public void StaffSendMessage(StaffMessage staffMessage)
        {
            try
            {
                using(var context = new GFDbContext())
                {
                    context.StaffMessages.Add(staffMessage);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.InnerException.Message);
            }
        }

        public void UserSendMessage(Message message)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Message.Add(message);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.InnerException.Message);
            }
        }

        public RoomChat CreateRoomChat(RoomChat room)
        {
            try
            {
                using(var context = new GFDbContext())
                {
                    context.RoomChat.Add(room);
                    context.SaveChanges();
                }
                return room;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.InnerException.Message);
            }
        }
    }
}
