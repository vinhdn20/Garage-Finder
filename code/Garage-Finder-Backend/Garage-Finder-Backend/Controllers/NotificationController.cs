using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Validations;
using Services.AuthService;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Security.Principal;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebSocketController : ControllerBase
    {
        private readonly JwtBearerOptions _jwtBearerOptions;
        private readonly IHubContext<UserGFHub> _hubContext;

        public WebSocketController(JwtBearerOptions jwtBearerOptions, IHubContext<UserGFHub> hubContext)
        {
            _jwtBearerOptions = jwtBearerOptions;
            _hubContext = hubContext;
        }

        [HttpPost("send/{message}")]
        public async Task<IActionResult> Send(string message)
        {
            await _hubContext.Clients.User("5").SendAsync("Notify", "BE", message);
            return Ok();
        }
    }
}
