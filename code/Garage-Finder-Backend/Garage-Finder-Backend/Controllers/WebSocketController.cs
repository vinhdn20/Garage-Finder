using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;

namespace Garage_Finder_Backend.Controllers
{
    public class WebSocketController : ControllerBase
    {
        [Authorize]
        [HttpGet("/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
        // </snippet>

        private static async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!receiveResult.CloseStatus.HasValue)
            {
                var sendbuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes("Whatever text you want to send"));
                if (receiveResult.Count > 0)
                {
                    string mess = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
                    dynamic json = JsonConvert.DeserializeObject<dynamic>(mess);
                    sendbuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes($"Đã nhận được: {json.type} || {json.message}"));
                    Console.WriteLine($"Đã nhận được: {json.type} || {json.message}");
                }
                await webSocket.SendAsync(
                    sendbuffer,
                    receiveResult.MessageType,
                    receiveResult.EndOfMessage,
                    CancellationToken.None);

                receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);
        }
    }
}
