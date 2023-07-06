using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthService
{
    [Authorize]
    [SignalRHub]
    public class UserGFHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "GF Users");
            await base.OnConnectedAsync();
        }
        [SignalRMethod("SendMessage")]
        public void SendMessage(string user, string message)
        {
            Console.WriteLine(user + "||" +message);
        }
    }
}
