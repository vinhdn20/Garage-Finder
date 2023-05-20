using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Implements;

namespace Garage_Finder_Backend.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IServiceRepository serviceRepository;

        public OrderController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IServiceRepository serviceRepository)
        {
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.serviceRepository = serviceRepository;
        }

        [HttpGet("GetAllOrder")]
        public IActionResult GetAll()
        {
            try
            {

                IEnumerable<OrdersDTO> orderList = orderRepository.GetAllOrders();
                foreach (OrdersDTO order in orderList)
                {
                    order.OrderDetail = orderDetailRepository.GetOrderDetailByOrderID(order.OrderID);
                }
                return Ok(orderList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetOrder/{id}")]
        public IActionResult GetId(int id)
        {

            try
            {

                OrdersDTO order = orderRepository.GetOrderById(id);
                order.OrderDetail = orderDetailRepository.GetOrderDetailByOrderID(order.OrderID);
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAllUserOrder/{userid}")]
        public IActionResult GetAll(int userid)
        {
            try
            {

                IEnumerable<OrdersDTO> orderList = orderRepository.GetAllOrdersByUserId(userid);
                foreach (OrdersDTO order in orderList)
                {
                    order.OrderDetail = orderDetailRepository.GetOrderDetailByOrderID(order.OrderID);
                    order.OrderDetail.CategoryID = serviceRepository.GetServiceById((int)order.OrderDetail.ServiceID).CategoryID;
                }
                return Ok(orderList);
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

                ServiceDTO orderService = serviceRepository.GetServiceById((int)newOrder.OrderDetail.ServiceID);

                newOrder.TimeCreate = DateTime.Now;
                newOrder.OrderDetail.NameService = orderService.NameService;
                newOrder.OrderDetail.Cost = orderService.Cost;

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
                orderDetailRepository.Delete(id);
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
