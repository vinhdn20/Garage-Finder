using DataAccess.DTO;
using DataAccess.DTO.Token;
using GFData.Models.Entity;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Repositories.Interfaces;
using Services.NotificationService;
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
        private readonly INotificationService _notificationService;

        public UserGFHub(IGarageRepository garageRepository, IStaffRepository staffRepository,
            INotificationService notificationService)
        {
            _garageRepository = garageRepository;
            _staffRepository = staffRepository;
            _notificationService = notificationService;
        }
        public override async Task OnConnectedAsync()
        {
            var user = GetTokenInfor();
            if (user.RoleName.Equals(Constants.ROLE_STAFF))
            {
                var staff = _staffRepository.GetById(user.UserID);
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Garage-{staff.GarageID}");
            }
            var garages = _garageRepository.GetGarageByUser(user.UserID);
            foreach (var garage in garages)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Garage-{garage.GarageID}");
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, $"GF-User");
            await base.OnConnectedAsync();
        }

        [SignalRMethod]
        public async Task ReadAllNotification()
        {
            var user = GetTokenInfor();
            _notificationService.ReadAllNotification(user.UserID, user.RoleName);
        }

        [SignalRMethod]
        public async Task<List<NotificationDTO>> GetAllNotification()
        {
            var user = GetTokenInfor();
            return _notificationService.GetNotification(user.UserID, user.RoleName);
        }

        private TokenInfor GetTokenInfor()
        {
            var jsonUser = Context.User.FindFirstValue("user");
            var user = JsonConvert.DeserializeObject<TokenInfor>(jsonUser);
            return user;
        }
    }
}
