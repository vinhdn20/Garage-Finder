using DataAccess.DTO.Token;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Repositories.Interfaces;
using System.Net.WebSockets;
using System.Text;

namespace Services.WebSocket
{
    public class GFWebSocketHandler : WebSocketHandler
    {

        protected readonly IGarageRepository _garageRepository;
        protected readonly IStaffRepository _staffRepository;
        protected readonly WebSocketFunction _webSocketFunction;

        public GFWebSocketHandler(WebSocketConnectionManager WebSocketConnectionManager, IHttpContextAccessor httpContextAccessor,
            IGarageRepository garageRepository, IStaffRepository staffRepository,
            WebSocketFunction webSocketFunction) : base(WebSocketConnectionManager, httpContextAccessor)
        {
            _garageRepository = garageRepository;
            _staffRepository = staffRepository;
            _webSocketFunction = webSocketFunction;
        }

        public override async Task OnConnected(System.Net.WebSockets.WebSocket socket)
        {
            try
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
            catch (Exception e)
            {

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
