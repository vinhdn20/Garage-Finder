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
        private readonly ConcurrentDictionary<string, string> _group = new ConcurrentDictionary<string, string>();
        
        public System.Net.WebSockets.WebSocket GetSocketById(string socketId)
        {
            return this._sockets.FirstOrDefault(x => x.Key == socketId).Value;
        }

        public List<System.Net.WebSockets.WebSocket> GetSocketByGroupId(string groupId)
        {
            var connectionIds = this._group.Where(x => x.Key == groupId).Select(x => x.Value).ToList();
            List<System.Net.WebSockets.WebSocket> sockets = new List<System.Net.WebSockets.WebSocket>();
            foreach (var connectionId in connectionIds)
            {
                sockets.AddRange(this._sockets.Where(x => x.Key.Equals(connectionId)).Select(x => x.Value).ToList());
            }
            return sockets;
        }

        public ConcurrentDictionary<string, System.Net.WebSockets.WebSocket> GetAllSockets()
        {
            return this._sockets;
        }

        public string GetSocketId(System.Net.WebSockets.WebSocket socket)
        {
            return this._sockets.FirstOrDefault(x => x.Value == socket).Key;
        }

        public string AddSocket(System.Net.WebSockets.WebSocket socket, TokenInfor user)
        {
            var connectionId = GenerateConnectionId();
            this._sockets.TryAdd(connectionId, socket);
            this._group.TryAdd(user.UserID.ToString(), connectionId);
            return connectionId;
        }

        public void AddToGroup(string connectionId, string groupName)
        {
            this._group.TryAdd(groupName, connectionId);
        }

        private string GenerateConnectionId()
        {
            return Guid.NewGuid().ToString("N");
        }

        public async Task RemoveSocket(string socketId)
        {
            try
            {

                this._sockets.TryRemove(socketId, out var socket);
                var removeSocket = this._group.Where(x => x.Value == socketId).ToList();
                if (removeSocket.Count > 0)
                {
                    removeSocket.ForEach(x => this._group.TryRemove(x.Key, out var connectionId));
                }
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed.", CancellationToken.None);
            }
            catch (Exception)
            {

            }
        }
    }
}
