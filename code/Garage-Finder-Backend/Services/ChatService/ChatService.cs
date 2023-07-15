using DataAccess.DTO.Chat;
using DataAccess.DTO.User;
using GFData.Models.Entity;
using Microsoft.AspNetCore.SignalR;
using Repositories.Interfaces;
using Services.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ChatService
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IHubContext<UserGFHub> _hubContext;

        public ChatService(IChatRepository chatRepository, IStaffRepository staffRepository,
            IUsersRepository usersRepository, IGarageRepository garageRepository,
            IHubContext<UserGFHub> hubContext)
        {
            _chatRepository = chatRepository;
            _staffRepository = staffRepository;
            _usersRepository = usersRepository;
            _hubContext = hubContext;
        }
        public List<ChatDTO> GetDetailMessage(int userId, string nameRole, int roomId)
        {
            List<ChatDTO> result = new List<ChatDTO>();
            var staffMessage = _chatRepository.GetStaffMessages(roomId);
            foreach (var mess in staffMessage)
            {
                ChatDTO chatDTO = new ChatDTO()
                {
                    Content = mess.Content,
                    DateTime = mess.DateTime,
                    IsRead = mess.IsRead,
                    IsSendByUser = false
                };
                result.Add(chatDTO);
            }
            var messages = _chatRepository.GetMessages(roomId);
            foreach (var mess in messages)
            {
                ChatDTO chatDTO = new ChatDTO()
                {
                    Content = mess.Content,
                    DateTime = mess.DateTime,
                    IsRead = mess.IsRead,
                    IsSendByUser = true
                };
                result.Add(chatDTO);
            }
            return result;
        }

        public List<RoomDTO> GetListRoom(int userId, string roleName)
        {
            List<RoomDTO> roomList = new List<RoomDTO>();
            if(roleName.Equals(Constants.ROLE_USER))
            {
                var rooms = _chatRepository.GetRoomByUser(userId);
                foreach (var room in rooms)
                {
                    RoomDTO roomDTO = new RoomDTO();
                    var messages = _chatRepository.GetMessages(room.RoomID).OrderBy(x => x.DateTime).FirstOrDefault();
                    var staffMess = _chatRepository.GetStaffMessages(room.RoomID).OrderBy(x => x.DateTime).FirstOrDefault();
                    UsersDTO user = new UsersDTO();
                    if(messages is null)
                    {
                        var staff = _staffRepository.GetById(staffMess.StaffId);
                        roomDTO.ToId = staff.GarageID;
                        user = _usersRepository.GetUserByID(staffMess.UserID);
                    }
                    else
                    {
                        roomDTO.ToId = messages.GarageID;
                        user = _usersRepository.GetUserByID(messages.UserID);
                    }

                    if(messages?.DateTime > staffMess?.DateTime)
                    {
                        roomDTO.DateTime = messages.DateTime;
                        roomDTO.IsRead = messages.IsRead;
                        roomDTO.Content = messages.Content;
                    }
                    else if (messages?.DateTime < staffMess?.DateTime)
                    {
                        roomDTO.DateTime = staffMess.DateTime;
                        roomDTO.IsRead = staffMess.IsRead;
                        roomDTO.Content = staffMess.Content;
                    }
                    else if(messages is null)
                    {
                        roomDTO.DateTime = staffMess.DateTime;
                        roomDTO.IsRead = staffMess.IsRead;
                        roomDTO.Content = staffMess.Content;
                    }
                    else if (staffMess is null)
                    {
                        roomDTO.DateTime = messages.DateTime;
                        roomDTO.IsRead = messages.IsRead;
                        roomDTO.Content = messages.Content;
                    }
                    roomDTO.RoomID = room.RoomID;
                    roomDTO.LinkImage = user.LinkImage;
                    roomDTO.Name = user.Name;
                    roomList.Add(roomDTO);
                }
            }
            else if(roleName.Equals(Constants.ROLE_STAFF))
            {
                var rooms = _chatRepository.GetStaffMessages(userId);
                foreach (var room in rooms)
                {
                    var messages = _chatRepository.GetMessages(room.RoomID).OrderBy(x => x.DateTime).FirstOrDefault();
                    var staffMess = _chatRepository.GetStaffMessages(room.RoomID).OrderBy(x => x.DateTime).FirstOrDefault();
                    var user = messages is null ? _usersRepository.GetUserByID(messages.UserID):
                                                    _usersRepository.GetUserByID(staffMess.UserID);
                    RoomDTO roomDTO = new RoomDTO();
                    if (messages?.DateTime > staffMess?.DateTime)
                    {
                        roomDTO.DateTime = messages.DateTime;
                        roomDTO.IsRead = messages.IsRead;
                        roomDTO.Content = messages.Content;
                    }
                    else if (messages?.DateTime < staffMess?.DateTime)
                    {
                        roomDTO.DateTime = staffMess.DateTime;
                        roomDTO.IsRead = staffMess.IsRead;
                        roomDTO.Content = staffMess.Content;
                    }
                    else if (messages is null)
                    {
                        roomDTO.DateTime = staffMess.DateTime;
                        roomDTO.IsRead = staffMess.IsRead;
                        roomDTO.Content = staffMess.Content;
                    }
                    else if (staffMess is null)
                    {
                        roomDTO.DateTime = messages.DateTime;
                        roomDTO.IsRead = messages.IsRead;
                        roomDTO.Content = messages.Content;
                    }
                    roomDTO.ToId = user.UserID;
                    roomDTO.RoomID = room.RoomID;
                    roomDTO.LinkImage = user.LinkImage;
                    roomDTO.Name = user.Name;
                    roomList.Add(roomDTO);
                }
            }
            return roomList;
        }

        public void SendToUser(int fromStaffId, SendChat sendChat)
        {
            RoomChat roomChat;
            var rooms = GetListRoom(fromStaffId, Constants.ROLE_STAFF);
            if (rooms.Any(x => x.ToId == sendChat.ToId))
            {
                roomChat = new RoomChat()
                {
                    RoomID = rooms.FirstOrDefault(x => x.ToId == sendChat.ToId).RoomID
                };
            }
            else
            {
                roomChat = _chatRepository.CreateRoomChat();
            }
            StaffMessage staffMessage = new StaffMessage()
            {
                Content = sendChat.Content,
                DateTime = DateTime.UtcNow,
                IsRead = false,
                StaffId = fromStaffId,
                UserID = sendChat.ToId,
                RoomID = roomChat.RoomID
            };
            _chatRepository.StaffSendMessage(staffMessage, sendChat.ToId);
            _hubContext.Clients.User(sendChat.ToId.ToString()).SendAsync("chat", staffMessage);
        }

        public void SendToGarage(int userId, SendChat sendChat)
        {
            RoomChat roomChat;
            var rooms = GetListRoom(userId, Constants.ROLE_USER);
            if (rooms.Any(x => x.ToId == sendChat.ToId))
            {
                roomChat = new RoomChat()
                {
                    RoomID = rooms.FirstOrDefault(x => x.ToId == sendChat.ToId).RoomID
                };
            }
            else
            {
                roomChat = _chatRepository.CreateRoomChat();
            }
            Message message = new Message()
            {
                Content = sendChat.Content,
                DateTime = DateTime.UtcNow,
                GarageID = sendChat.ToId,
                IsRead = false,
                UserID = userId,    
                RoomID = roomChat.RoomID
            };
            _chatRepository.UserSendMessage(message, sendChat.ToId);
            _hubContext.Clients.Group($"Garage-{sendChat.ToId}").SendAsync("chat", message);
        }
    }
}
