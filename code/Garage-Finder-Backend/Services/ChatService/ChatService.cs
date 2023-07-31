using DataAccess.DTO.Chat;
using DataAccess.DTO.User;
using GFData.Models.Entity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Repositories.Implements.Garage;
using Repositories.Interfaces;
using Services.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Voice;

namespace Services.ChatService
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IGarageRepository _garageRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IHubContext<UserGFHub> _hubContext;
        private readonly WebsocketSend _webSocketHandler;

        public ChatService(IChatRepository chatRepository, IStaffRepository staffRepository,
            IUsersRepository usersRepository, IGarageRepository garageRepository,
            IHubContext<UserGFHub> hubContext, WebsocketSend webSocket)
        {
            _chatRepository = chatRepository;
            _staffRepository = staffRepository;
            _usersRepository = usersRepository;
            _garageRepository = garageRepository;
            _hubContext = hubContext;
            _webSocketHandler = webSocket;
        }
        public List<ChatDTO> GetDetailMessage(int userId, string nameRole, int roomId)
        {
            List<ChatDTO> result = new List<ChatDTO>();
            var staffMessage = _chatRepository.GetStaffMessages(roomId);
            var room = _chatRepository.GetRoomById(roomId);
            foreach (var mess in staffMessage)
            {
                var isSendByMe = false;
                if (nameRole.Equals(Constants.ROLE_STAFF))
                {
                    isSendByMe = true;
                }
                else
                {
                    var garages = _garageRepository.GetGarageByUser(userId);
                    if(garages.Any(x => x.GarageID == room.GarageID))
                    {
                        isSendByMe = true;
                    }
                }
                ChatDTO chatDTO = new ChatDTO()
                {
                    RoomID = roomId,
                    Content = mess.Content,
                    DateTime = mess.DateTime,
                    IsSendByMe = isSendByMe
                };
                result.Add(chatDTO);
            }
            var messages = _chatRepository.GetMessages(roomId);
            foreach (var mess in messages)
            {
                var isSendByMe = false;
                if (nameRole.Equals(Constants.ROLE_USER) && mess.UserID == userId)
                {
                    isSendByMe = true;
                }
                ChatDTO chatDTO = new ChatDTO()
                {
                    RoomID = roomId,
                    Content = mess.Content,
                    DateTime = mess.DateTime,
                    IsSendByMe = isSendByMe
                };
                result.Add(chatDTO);
            }
            result = result.OrderBy(x => x.DateTime).ToList();
            return result;
        }

        public void SendToUser(int userId, string nameRole, SendChatToUserByGarage sendChat)
        {
            RoomChat roomChat;
            if(nameRole.Equals(Constants.ROLE_USER))
            {
                var user = _usersRepository.GetUserByID(userId);
                var rooms = _chatRepository.GetRoomByGarageId(sendChat.FromGarageId);
                
                if (rooms.Any(x => x.UserID == sendChat.UserID))
                {
                    roomChat = new RoomChat()
                    {
                        RoomID = rooms.First(x => x.UserID == sendChat.UserID).RoomID
                    };
                }
                else
                {
                    RoomChat room = new RoomChat()
                    {
                        GarageID = sendChat.FromGarageId,
                        UserID = sendChat.UserID
                    };
                    roomChat = _chatRepository.CreateRoomChat(room);
                }
                Message message = new Message()
                {
                    Content = sendChat.Content,
                    DateTime = DateTime.UtcNow,
                    UserID = userId,
                    RoomID = roomChat.RoomID
                };
                _chatRepository.UserSendMessage(message, sendChat.UserID);
                //_hubContext.Clients.User(sendChat.UserID.ToString()).SendAsync("chat", message);
                _webSocketHandler.SendAsync(sendChat.UserID.ToString(), "chat", message);
            }
            else
            {
                var staff = _staffRepository.GetById(userId);
                if(staff.GarageID != sendChat.FromGarageId)
                {
                    throw new Exception("Nhân viên không thuộc garage này!");
                }
                var rooms = _chatRepository.GetRoomByGarageId(sendChat.FromGarageId);
                if (rooms.Any(x => x.UserID == sendChat.UserID))
                {
                    roomChat = new RoomChat()
                    {
                        RoomID = rooms.First(x => x.UserID == sendChat.UserID).RoomID
                    };
                }
                else
                {
                    RoomChat room = new RoomChat()
                    {
                        GarageID = staff.GarageID,
                        UserID = sendChat.UserID
                    };
                    roomChat = _chatRepository.CreateRoomChat(room);
                }
                StaffMessage staffMessage = new StaffMessage()
                {
                    Content = sendChat.Content,
                    DateTime = DateTime.UtcNow,
                    StaffId = userId,
                    RoomID = roomChat.RoomID
                };
                _chatRepository.StaffSendMessage(staffMessage, sendChat.UserID);
                //_hubContext.Clients.User(sendChat.UserID.ToString()).SendAsync("chat", staffMessage);

                _webSocketHandler.SendAsync(sendChat.UserID.ToString(), "chat", staffMessage);
            }
            
        }

        public void SendToGarage(int userId, SendChatToGarage sendChat)
        {
            RoomChat roomChat;
            var rooms = GetListRoomByUserId(userId);
            if (rooms.Any(x => x.GarageID == sendChat.GarageID))
            {
                roomChat = new RoomChat()
                {
                    RoomID = rooms.First(x => x.GarageID == sendChat.GarageID).RoomID
                };
            }
            else
            {
                RoomChat room = new RoomChat()
                {
                    UserID = userId,
                    GarageID = sendChat.GarageID
                };
                roomChat = _chatRepository.CreateRoomChat(room);
            }
            Message message = new Message()
            {
                Content = sendChat.Content,
                DateTime = DateTime.UtcNow,
                UserID = userId,
                RoomID = roomChat.RoomID
            };
            _chatRepository.UserSendMessage(message, sendChat.GarageID);
            //_hubContext.Clients.Group($"Garage-{sendChat.GarageID}").SendAsync("chat", message);
            _webSocketHandler.SendToGroup($"Garage-{sendChat.GarageID}", "chat", message);
        }

        public List<RoomDTO> GetListRoomByUserId(int userId)
        {
            List<RoomDTO> roomList = new List<RoomDTO>();
            var rooms = _chatRepository.GetRoomByUser(userId);
            foreach (var room in rooms)
            {
                RoomDTO roomDTO = new RoomDTO();
                var messages = _chatRepository.GetMessages(room.RoomID).OrderByDescending(x => x.DateTime).FirstOrDefault();
                var staffMess = _chatRepository.GetStaffMessages(room.RoomID).OrderByDescending(x => x.DateTime).FirstOrDefault();
                UsersDTO user = _usersRepository.GetUserByID(room.UserID);

                if (messages?.DateTime > staffMess?.DateTime)
                {
                    roomDTO.DateTime = messages.DateTime;
                    roomDTO.Content = messages.Content;
                }
                else if (messages?.DateTime < staffMess?.DateTime)
                {
                    roomDTO.DateTime = staffMess.DateTime;
                    roomDTO.Content = staffMess.Content;
                }
                else if (messages is null)
                {
                    roomDTO.DateTime = staffMess.DateTime;
                    roomDTO.Content = staffMess.Content;
                }
                else if (staffMess is null)
                {
                    roomDTO.DateTime = messages.DateTime;
                    roomDTO.Content = messages.Content;
                }
                roomDTO.RoomID = room.RoomID;
                roomDTO.UserID = room.UserID;
                roomDTO.GarageID = room.GarageID;
                var garage = _garageRepository.GetGaragesByID(roomDTO.GarageID);
                roomDTO.Name = garage.GarageName;
                roomDTO.LinkImage = garage.Thumbnail;
                roomList.Add(roomDTO);
            }
            return roomList;
        }

        public List<RoomDTO> GetListRoomByGarageId(int userId, int garageId)
        {
            List<RoomDTO> roomList = new List<RoomDTO>();
            var rooms = _chatRepository.GetRoomByGarageId(garageId);
            foreach (var room in rooms)
            {
                var messages = _chatRepository.GetMessages(room.RoomID).OrderByDescending(x => x.DateTime).FirstOrDefault();
                var staffMess = _chatRepository.GetStaffMessages(room.RoomID).OrderByDescending(x => x.DateTime).FirstOrDefault();
                var user = _usersRepository.GetUserByID(room.UserID);
                RoomDTO roomDTO = new RoomDTO();
                if (messages?.DateTime > staffMess?.DateTime)
                {
                    roomDTO.DateTime = messages.DateTime;
                    roomDTO.Content = messages.Content;
                }
                else if (messages?.DateTime < staffMess?.DateTime)
                {
                    roomDTO.DateTime = staffMess.DateTime;
                    roomDTO.Content = staffMess.Content;
                }
                else if (messages is null)
                {
                    roomDTO.DateTime = staffMess.DateTime;
                    roomDTO.Content = staffMess.Content;
                }
                else if (staffMess is null)
                {
                    roomDTO.DateTime = messages.DateTime;
                    roomDTO.Content = messages.Content;
                }
                roomDTO.UserID = user.UserID;
                roomDTO.GarageID = room.GarageID;
                roomDTO.RoomID = room.RoomID;
                roomDTO.LinkImage = user.LinkImage;
                roomDTO.Name = user.Name;
                roomList.Add(roomDTO);
            }
            return roomList;
        }

        public void SendMessageToUser(int senderUserId, int recieverUserId, string content)
        {
            MessageToUser messageToUser = new MessageToUser()
            {
                Content = content,
                DateTime = DateTime.UtcNow,
                ReceiverUserID = recieverUserId,
                SenderUserID = senderUserId,
            };
            _chatRepository.SendMessageToUsers(messageToUser);
            //_hubContext.Clients.User(recieverUserId.ToString()).SendAsync("chatWithUser", messageToUser);

            _webSocketHandler.SendAsync(recieverUserId.ToString(), "chatWithUser", messageToUser);
        }

        public List<ChatDTO> GetMessageWithUser(int userId, int userId2)
        {
            List<ChatDTO> chats = new List<ChatDTO>();
            var sendMess = _chatRepository.GetMessagesToUsers(userId, userId2);
            var receiveMess = _chatRepository.GetMessagesToUsers(userId2, userId);
            foreach (var mess in sendMess)
            {
                var isSendByMe = false;
                if (mess.SenderUserID == userId)
                {
                    isSendByMe = true;
                }
                ChatDTO chatDTO = new ChatDTO()
                {
                    Content = mess.Content,
                    DateTime = mess.DateTime,
                    IsSendByMe = isSendByMe
                };
                chats.Add(chatDTO);
            }

            foreach (var mess in receiveMess)
            {
                var isSendByMe = false;
                if (mess.SenderUserID == userId)
                {
                    isSendByMe = true;
                }
                ChatDTO chatDTO = new ChatDTO()
                {
                    Content = mess.Content,
                    DateTime = mess.DateTime,
                    IsSendByMe = isSendByMe
                };
                chats.Add(chatDTO);
            }
            chats = chats.OrderBy(x => x.DateTime).ToList();
            return chats;
        }
    }
}
