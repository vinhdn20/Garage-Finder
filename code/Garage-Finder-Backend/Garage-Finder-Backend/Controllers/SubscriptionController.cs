using DataAccess.DTO.Subscription;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : Controller
    {
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

        [HttpGet("getLinkPay")]
        public IActionResult GetLink(int subscribeID)
        {
            try
            {
                return Ok("Chưa xử lý");
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


                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
