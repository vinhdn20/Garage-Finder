using DataAccess.DTO.Chat;
using DataAccess.DTO;
using DataAccess.DTO.Token;
using Newtonsoft.Json;
using SignalRSwaggerGen.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Repositories.Interfaces;
using Services.NotificationService;
using Services.ChatService;
using Repositories.Implements.Garage;
using Repositories.Implements.StaffRepository;
using System.Text.RegularExpressions;
using GFData.Models.Entity;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Azure;

namespace Services.WebSocket
{
    public class GFWebSocketHandler : WebSocketHandler
    {

        protected readonly IGarageRepository _garageRepository;
        protected readonly IStaffRepository _staffRepository;
        //protected readonly WebsocketSend _websocketSend;
        protected readonly WebSocketFunction _webSocketFunction;

        //private readonly IChatService _chatService;
        //private readonly INotificationService _notificationService;
        public GFWebSocketHandler(WebSocketConnectionManager WebSocketConnectionManager, IHttpContextAccessor httpContextAccessor,
            IGarageRepository garageRepository, IStaffRepository staffRepository,
            //WebsocketSend websocketSend,
            WebSocketFunction webSocketFunction) : base(WebSocketConnectionManager, httpContextAccessor)
        {
            _garageRepository = garageRepository;
            _staffRepository = staffRepository;
            //_chatService = chatService;
            //_notificationService = notificationService;
            //_websocketSend = websocketSend;
            _webSocketFunction = webSocketFunction;
        }

        public override async Task OnConnected(System.Net.WebSockets.WebSocket socket)
        {
            var user = HttpContextAccessor.HttpContext.User.GetTokenInfor();
            var connectionId = this.WebSocketConnectionManager.AddSocket(socket, user);
            if (user.RoleName.Equals(Constants.ROLE_STAFF))
            {
                var staff = _staffRepository.GetById(user.UserID);
                WebSocketConnectionManager.AddToGroup(connectionId, $"Garage-{staff.GarageID}");
            }
            var garages = _garageRepository.GetGarageByUser(user.UserID);
            foreach (var garage in garages)
            {
                WebSocketConnectionManager.AddToGroup(connectionId, $"Garage-{garage.GarageID}");
            }
        }

        public override Task OnDisconnected(System.Net.WebSockets.WebSocket socket)
        {
            try
            {
                return base.OnDisconnected(socket);
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }
        public override async Task ReceiveAsync(System.Net.WebSockets.WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            try
            {
                if (HttpContextAccessor == null)
                {
                    return;
                }
                var user = HttpContextAccessor.HttpContext.User.GetTokenInfor();
                string mess = Encoding.UTF8.GetString(buffer, 0, result.Count);
                dynamic json = JsonConvert.DeserializeObject<dynamic>(mess);
                ArraySegment<byte> sendbuffer = new ArraySegment<byte>();
                var namedMethod = (string)json.type;
                var method = _webSocketFunction.GetType().GetMethod(namedMethod);
                if (method != null)
                {
                    var parameters = method.GetParameters();
                    object[] args = new object[parameters.Length];
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (parameters[i].ParameterType == typeof(TokenInfor))
                        {
                            args[i] = user;
                        }
                        else if (parameters[i].ParameterType == typeof(int))
                        {
                            var canConvert = int.TryParse(JsonConvert.SerializeObject(json.message), out int number);
                            if (canConvert)
                            {
                                args[i] = number;
                            }
                        }
                        else if (parameters[i].ParameterType == typeof(string))
                        {
                            var message = (string)json.message;

                            args[i] = message;
                        }
                        else
                        {
                            args[i] = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(json.message), parameters[1].ParameterType);
                        }
                    }
                    object list = null;
                    list = method.Invoke(_webSocketFunction, args);
                    if (list != null)
                    {
                        sendbuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(list)));
                    }
                }

                if (sendbuffer.Count > 0)
                {
                    await socket.SendAsync(
                    sendbuffer,
                    WebSocketMessageType.Text,
                    WebSocketMessageFlags.EndOfMessage,
                    CancellationToken.None);
                }
            }
            catch (Exception e)
            {

            }
        }

    }
}
