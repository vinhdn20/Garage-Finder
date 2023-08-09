using DataAccess.DTO.Subscription;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Repositories.Implements.UserRepository;
using Repositories.Interfaces;
using Services;
using Services.SubcriptionService;
using Twilio.TwiML.Voice;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : Controller
    {
        private readonly ISubcriptionService _subcriptionService;
        private readonly IUsersRepository _usersRepository;
        public SubscriptionController(ISubcriptionService subcriptionService, IUsersRepository usersRepository)
        {
            _subcriptionService = subcriptionService;
            _usersRepository = usersRepository;
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                var subs = _subcriptionService.GetAll();
                return Ok(subs);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("add")]
        [Authorize]
        public IActionResult Add(AddSubcribeDTO addSubcribe)
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                _subcriptionService.Add(addSubcribe);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("update")]
        [Authorize]
        public IActionResult Update(SubscribeDTO subscribe)
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                _subcriptionService.Update(subscribe);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[HttpDelete("block")]
        //[Authorize(Roles = $"{Constants.ROLE_ADMIN}")]
        //public IActionResult Block(int id)
        //{
        //    try
        //    {
        //        _subcriptionService.Block(id);
        //        return Ok("SUCCESS");
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        //[HttpPost("unblock")]
        //[Authorize(Roles = $"{Constants.ROLE_ADMIN}")]
        //public IActionResult UnBlock(int id)
        //{
        //    try
        //    {
        //        _subcriptionService.UnBlock(id);
        //        return Ok("SUCCESS");
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        [HttpPost("getLinkPay")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult GetLink(int subscribeID, int garageId)
        {
            try
            {
                var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
                string ip = "127.0.0.1";
                if (!ipAddress.ToString().Equals("::1"))
                {
                    ip = ipAddress.Address.ToString();
                }
                var user = User.GetTokenInfor();
                return _subcriptionService.GetLinkPay(user.UserID, garageId, subscribeID, ip);
                //return Ok(link);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("registeredSubscription/{garageId}")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult GetRegistered(int garageId)
        {
            try
            {
                var user = User.GetTokenInfor();
                var invoices = _subcriptionService.GetInvoicesByGarageId(user.UserID,garageId);

                return Ok(invoices);
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
                _subcriptionService.UpdateInvoice(vNPay);
                
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAllInvoice")]
        [Authorize]
        public IActionResult GetAllInvoice()
        {
            try
            {

                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                var invoices = _subcriptionService.GetAllInvoices();

                return Ok(invoices);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetIncome")]
        [Authorize]
        public IActionResult GetIncome()
        {
            try
            {

                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                double income = _subcriptionService.GetIncome();

                return Ok(income);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        private bool CheckAdmin()
        {
            var user = User.GetTokenInfor();
            var userDTO = _usersRepository.GetUserByID(user.UserID);
            if (!userDTO.roleName.NameRole.Equals(Constants.ROLE_ADMIN))
            {
                return false;
            }
            return true;
        }
    }
}
