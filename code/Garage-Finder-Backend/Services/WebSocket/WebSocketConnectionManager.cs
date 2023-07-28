using DataAccess.DTO.Token;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace Services.WebSocket
{
    public class WebSocketConnectionManager
    {
        private readonly ConcurrentDictionary<string, System.Net.WebSockets.WebSocket> _sockets = new ConcurrentDictionary<string, System.Net.WebSockets.WebSocket>();
        private readonly ConcurrentDictionary<string, System.Net.WebSockets.WebSocket> _group = new ConcurrentDictionary<string, System.Net.WebSockets.WebSocket>();
        
        public System.Net.WebSockets.WebSocket GetSocketById(string socketId)
        {
            return this._sockets.FirstOrDefault(x => x.Key == socketId).Value;
        }

        public List<System.Net.WebSockets.WebSocket> GetSocketByGroupId(string groupId)
        {
            return this._sockets.Where(x => x.Key == groupId).Select(x => x.Value).ToList();
        }

        public ConcurrentDictionary<string, System.Net.WebSockets.WebSocket> GetAllSockets()
        {
            return this._sockets;
        }

        public string GetSocketId(System.Net.WebSockets.WebSocket socket)
        {
            return this._sockets.FirstOrDefault(x => x.Value == socket).Key;
        }

        public void AddSocket(System.Net.WebSockets.WebSocket socket, TokenInfor user)
        {
            this._sockets.TryAdd(user.UserID.ToString(), socket);
        }

        public void AddToGroup(System.Net.WebSockets.WebSocket socket, string groupName)
        {
            this._group.TryAdd(groupName, socket);
        }

        //private string GenerateConnectionId()
        //{
        //    return Guid.NewGuid().ToString("N");
        //}

        public async Task RemoveSocket(string socketId)
        {
            this._sockets.TryRemove(socketId, out var socket);
            var removeSocket = this._group.FirstOrDefault(x => x.Value == socket);
            if(removeSocket.Key is not null)
            {
                this._group.TryRemove(removeSocket.Key, out socket);
            }
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed.", CancellationToken.None);
        }
    }
}
