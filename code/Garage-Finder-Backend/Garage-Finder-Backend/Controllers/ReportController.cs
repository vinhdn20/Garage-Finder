using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Repositories.Interfaces;
using Services;
using DataAccess.DTO.Report;
using Services.ReportService;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;
        private readonly IUsersRepository usersRepository;
        public ReportController(IReportService reportService, IUsersRepository usersRepository)
        {
            this.reportService = reportService;
            this.usersRepository = usersRepository;
        }
        [HttpGet("GetListReport")]
        [Authorize]
        public IActionResult GetListReport()
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                var result = reportService.GetList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }

        }

        [HttpPost("AddReport")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult AddReport([FromBody] AddReportDTO report)
        {
            try
            {
                var user = User.GetTokenInfor();
                reportService.AddReport(report, user.UserID);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }

        [HttpGet("GetReportByID")]
        [Authorize]
        public IActionResult GetReportByID(int id)
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                var result = reportService.GetByID(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }

        }

        private bool CheckAdmin()
        {
            var user = User.GetTokenInfor();
            var userDTO = usersRepository.GetUserByID(user.UserID);
            if (!userDTO.roleName.NameRole.Equals(Constants.ROLE_ADMIN))
            {
                return false;
            }
            return true;
        }
    }
}
