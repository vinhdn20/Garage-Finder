using DataAccess.DTO.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Repositories.Implements.Garage;
using Repositories.Interfaces;
using Services.ChatService;
using Services.NotificationService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.WebSocket
{
    public abstract class WebSocketHandler
    {
        protected WebSocketConnectionManager WebSocketConnectionManager { get; set; }
        protected IHttpContextAccessor HttpContextAccessor;

        public WebSocketHandler(WebSocketConnectionManager WebSocketConnectionManager, IHttpContextAccessor httpContextAccessor)
        {
            this.WebSocketConnectionManager = WebSocketConnectionManager;
            this.HttpContextAccessor = httpContextAccessor;
        }

        public virtual async Task OnConnected(System.Net.WebSockets.WebSocket socket)
        {
            var user  = HttpContextAccessor.HttpContext.User.GetTokenInfor();
            this.WebSocketConnectionManager.AddSocket(socket, user);
        }
            
        public virtual async Task OnDisconnected(System.Net.WebSockets.WebSocket socket)
        {
            var id = this.WebSocketConnectionManager.GetSocketId(socket);
            await this.WebSocketConnectionManager.RemoveSocket(id);
        }

        public async Task SendMessageAsync(System.Net.WebSockets.WebSocket socket, string message)
        {
            Debug.Print(message);
            if (socket.State != WebSocketState.Open) { return; }
            var bytes = Encoding.UTF8.GetBytes(message);
            var buffer = new ArraySegment<byte>(bytes, 0, bytes.Length);
            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }
        public abstract Task ReceiveAsync(System.Net.WebSockets.WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}
