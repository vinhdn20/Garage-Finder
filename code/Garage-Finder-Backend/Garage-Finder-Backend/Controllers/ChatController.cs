using DataAccess.DTO.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.ChatService;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("getDetailMessage/{roomId}")]
        [Authorize(Roles = $"{Constants.ROLE_USER}, {Constants.ROLE_STAFF}")]
        public IActionResult GetDetailMessage(int roomId)
        {
            try
            {
                var user = User.GetTokenInfor();
                var result= _chatService.GetDetailMessage(user.UserID, user.RoleName,roomId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("sendToUser")]
        [Authorize(Roles = $"{Constants.ROLE_USER}, {Constants.ROLE_STAFF}")]
        public IActionResult SendToUser(SendChatToUserByGarage sendChat)
        {
            try
            {
                var user = User.GetTokenInfor();
                _chatService.SendToUser(user.UserID, user.RoleName, sendChat);
                return Ok("success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("sendToGarage")]
        [Authorize(Roles = $"{Constants.ROLE_USER}, {Constants.ROLE_STAFF}")]
        public IActionResult SendToGarage(SendChatToGarage sendChat)
        {
            try
            {
                var user = User.GetTokenInfor();
                _chatService.SendToGarage(user.UserID,sendChat);
                return Ok("success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getListRoomByUserId")]
        [Authorize(Roles = $"{Constants.ROLE_USER}, {Constants.ROLE_STAFF}")]
        public IActionResult GetListRoomByUserId()
        {
            try
            {
                var user = User.GetTokenInfor();
                var result = _chatService.GetListRoomByUserId(user.UserID);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getListRoomByGarageId/{garageId}")]
        [Authorize(Roles = $"{Constants.ROLE_USER}, {Constants.ROLE_STAFF}")]
        public IActionResult GetListRoomByGarageId(int garageId)
        {
            try
            {
                var user = User.GetTokenInfor();
                var result = _chatService.GetListRoomByGarageId(user.UserID, garageId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("sendMessageToUser")]
        [Authorize(Roles = $"{Constants.ROLE_USER}, {Constants.ROLE_STAFF}")]
        public IActionResult SendMessageToUser(SendChatToUser sendChat)
        {
            try
            {
                var user = User.GetTokenInfor();
                _chatService.SendMessageToUser(user.UserID, sendChat.ToUserId, sendChat.Content);
                return Ok("success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getMessageWithUser/{userId}")]
        [Authorize(Roles = $"{Constants.ROLE_USER}, {Constants.ROLE_STAFF}")]
        public IActionResult GetMessageWithUser(int userId)
        {
            try
            {
                var user = User.GetTokenInfor();
                var result = _chatService.GetMessageWithUser(user.UserID, userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
