using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IOrderRepository orderRepository;

        public ServiceController(IServiceRepository serviceRepository, IOrderRepository orderRepository)
        {
            this.serviceRepository = serviceRepository;
            this.orderRepository = orderRepository;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(serviceRepository.GetServices());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetId(int id)
        {
            try
            {
                return Ok(serviceRepository.GetServiceById(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetByCategory/{id}")]
        public IActionResult GetCategoryId(int id)
        {
            try
            {
                return Ok(serviceRepository.GetServicesByCategory(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("Add")]
        public IActionResult Add(ServiceDTO service)
        {
            try
            {
                serviceRepository.SaveService(service);

                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(ServiceDTO service)
        {
            try
            {
                serviceRepository.UpdateService(service);
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
                serviceRepository.DeleteService(id);

               /* IEnumerable<OrdersDTO> orderList = orderRepository.GetAllOrders();

                foreach (OrdersDTO order in orderList)
                {
                    order.OrderID = orderDetailRepository.GetOrderDetailByOrderID(order.OrderID);
                    if (order.OrderDetail == null)
                    {
                        orderRepository.Delete(order.OrderID);
                    }
                }*/

                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
