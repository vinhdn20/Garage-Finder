using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories.Implements;
using Repositories.Interfaces;

namespace Garage_Finder_Backend.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IServiceRepository serviceRepository;

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

        [HttpGet("GetByUser/{id}")]
        public IActionResult GetUserId(int id)
        {
            try
            {
                return Ok(orderRepository.GetAllOrdersByUserId(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpPost("AddOrder")]
        public IActionResult Add(OrdersDTO newOrder)
        {

            try
            {

                ServiceDTO orderService = serviceRepository.GetServiceById((int)newOrder.ServiceID);

                newOrder.TimeCreate = DateTime.Now;
                newOrder.ServiceID = orderService.ServiceID;
                serviceRepository.UpdateService(orderService);
                orderRepository.Add(newOrder);

                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
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
