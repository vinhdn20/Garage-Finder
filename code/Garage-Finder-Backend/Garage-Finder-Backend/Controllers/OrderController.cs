using DataAccess.DTO;
using DataAccess.DTO.ResponeModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.Implements;
using Repositories.Interfaces;
using System.Security.Claims;

namespace Garage_Finder_Backend.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IServiceRepository serviceRepository;
        private UserInfor GetUserFromToken()
        {
            var jsonUser = User.FindFirstValue("user");
            var user = JsonConvert.DeserializeObject<UserInfor>(jsonUser);
            return user;
        }
        public OrderController(IOrderRepository orderRepository, IServiceRepository serviceRepository)
        {
            this.orderRepository = orderRepository;
            this.serviceRepository = serviceRepository;
        }

        [HttpGet("GetAllOrder")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(orderRepository.GetAllOrders());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetByUser")]
        [Authorize]
        public IActionResult GetUserId()
        {
            try
            {
                var user = GetUserFromToken();
                return Ok(orderRepository.GetAllOrdersByUserId(user.UserID));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpPost("AddOrder")]
        public IActionResult Add([FromBody] OrdersDTO newOrder)
        {

            try
            {

                ServiceDTO orderService = serviceRepository.GetServiceById((int)newOrder.ServiceID);

                newOrder.TimeCreate = DateTime.Now;
                orderRepository.Add(newOrder);

                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete([FromBody] int id)
        {
            try
            {
                orderRepository.Delete(id);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
