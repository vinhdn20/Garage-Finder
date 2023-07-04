using DataAccess.DTO.Feedback;
using DataAccess.DTO.User.ResponeModels;
using GFData.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.Interfaces;
using Services.FeedbackService;
using System.Security.Claims;

namespace Garage_Finder_Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet("GetByGarage/{id}")]
        public IActionResult GetGarageId(int id)
        {
            try
            {
                var list = _feedbackService.GetFeedbackByGarage(id);
                return Ok(list);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("Add")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult Add(AddFeedbackDTO feedback)
        {
            try
            {
                var user = User.GetTokenInfor();
                _feedbackService.AddFeedback(feedback, user.UserID);

                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
