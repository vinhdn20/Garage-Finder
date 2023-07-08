using DataAccess.DTO.Token;
using GFData.Models.Entity;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Repositories.Interfaces;
using SignalRSwaggerGen.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.WebSocket
{
    [Authorize]
    [SignalRHub]
    public class UserGFHub : Hub
    {
        private readonly IGarageRepository _garageRepository;
        private readonly IStaffRepository _staffRepository;

        public UserGFHub(IGarageRepository garageRepository, IStaffRepository staffRepository)
        {
            _garageRepository = garageRepository;
            _staffRepository = staffRepository;
        }
        public override async Task OnConnectedAsync()
        {
            var jsonUser = Context.User.FindFirstValue("user");
            var user = JsonConvert.DeserializeObject<TokenInfor>(jsonUser);
            if (user.RoleName.Equals(Constants.ROLE_STAFF))
            {
                var staff = _staffRepository.GetById(user.UserID);
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Garage-{staff.GarageID}");
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, $"GF-User");
            await base.OnConnectedAsync();
        }
    }
}
