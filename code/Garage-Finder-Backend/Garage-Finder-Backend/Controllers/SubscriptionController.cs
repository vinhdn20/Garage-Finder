using DataAccess.DTO.Subscription;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Services.SubcriptionService;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : Controller
    {
        private readonly ISubcriptionService _subcriptionService;
        public SubscriptionController(ISubcriptionService subcriptionService)
        {
            _subcriptionService = subcriptionService;
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            return View();
        }

        [HttpPost("add")]
        [Authorize(Roles = $"{Constants.ROLE_ADMIN}")]
        public IActionResult Add()
        {
            try
            {
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("update")]
        [Authorize(Roles = $"{Constants.ROLE_ADMIN}")]
        public IActionResult Update()
        {
            try
            {
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete")]
        [Authorize(Roles = $"{Constants.ROLE_ADMIN}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("addInvoices")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult AddInvoices()
        {
            try
            {
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("getLinkPay")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult GetLink(int subscribeID, int garageId)
        {
            try
            {
                var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
                string ip = "192.168.1.146";
                if (!ipAddress.ToString().Equals("::1"))
                {
                    ip = ipAddress.Address.ToString();
                }
                var user = User.GetTokenInfor();
                var link =  _subcriptionService.GetLinkPay(user.UserID, garageId, subscribeID, ip);
                return Ok(link);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("vnpay_ipn")]
        public IActionResult VPN_IPN([FromQuery] VNPayIPNDTO vNPay)
        {
            try
            {
                _subcriptionService.AddInvoice(vNPay);
                
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
