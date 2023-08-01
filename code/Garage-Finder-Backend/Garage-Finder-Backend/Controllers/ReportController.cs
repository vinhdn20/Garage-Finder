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
        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }
        [HttpGet("GetListReport")]
        public IActionResult GetListReport()
        {
            try
            {
                var result = reportService.GetList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }

        }

        [HttpPost("AddReport")]
        public IActionResult AddReport([FromBody] AddReportDTO report)
        {
            try
            {
                var user = User.GetTokenInfor();
                reportService.AddReport(report, report.UserID);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }

        [HttpGet("GetReportByID")]
        public IActionResult GetReportByID(int id)
        {
            try
            {
                var result = reportService.GetByID(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }

        }
    }
}
