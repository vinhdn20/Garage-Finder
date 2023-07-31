using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Services.WebSocket
{
    public class WebsocketSend
    {
        protected WebSocketConnectionManager WebSocketConnectionManager { get; set; }
        protected IHttpContextAccessor HttpContextAccessor;

        public WebsocketSend(WebSocketConnectionManager WebSocketConnectionManager, IHttpContextAccessor httpContextAccessor)
        {
            this.WebSocketConnectionManager = WebSocketConnectionManager;
            this.HttpContextAccessor = httpContextAccessor;
        }

        public async void SendAsync(string userId, string method, object obj)
        {
            var socket = WebSocketConnectionManager.GetSocketById(userId.ToString());
            if(socket == null)
            {
                return;
            }
            dynamic sendObj = new { type = method, message = obj };
            var sendbuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendObj)));
            await socket.SendAsync(
                sendbuffer,
                WebSocketMessageType.Text,
                endOfMessage: true,
                CancellationToken.None);
        }

        public async void SendToGroup(string groupId, string method, object obj)
        {
            var sockets = WebSocketConnectionManager.GetSocketByGroupId(groupId);
            dynamic sendObj = new { type = method, message = obj };
            var sendbuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendObj)));
            List<Task> tasks = new List<Task>();
            foreach (var socket in sockets)
            {
                if (socket == null)
                {
                    continue;
                }
                //tasks.Add(socket.SendAsync(
                //    sendbuffer,
                //    WebSocketMessageType.Text,
                //    endOfMessage: true,
                //    CancellationToken.None));
                await socket.SendAsync(
                    sendbuffer,
                    WebSocketMessageType.Text,
                    endOfMessage: true,
                    CancellationToken.None);
            }
            //if(tasks.Count > 0)
            //    Task.W(tasks.ToArray());
        }
    }
}
