using DataAccess.DTO.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ChatService;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("getListRoom")]
        [Authorize]
        public IActionResult GetListRoom()
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getDetailMessage/{roomId}")]
        [Authorize]
        public IActionResult GetDetailMessage(int roomId)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("userSendMessage")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult UserSendMessage([FromBody]SendChatToGarage sendChat)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("staffSendMessage")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult StaffSendMessage([FromBody] SendChatToUser sendChat)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
