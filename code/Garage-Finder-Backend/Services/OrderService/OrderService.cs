using DataAccess.DTO;
using DataAccess.DTO.RequestDTO.Order;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderService
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICarRepository _carRepository;

        public OrderService(IOrderRepository orderRepository, ICarRepository carRepository)
        {
            _orderRepository = orderRepository;
            _carRepository = carRepository;
        }

        public void AddOrderWithCar(AddOrderWithCarDTO addOrder)
        {
            // Todo: validate
            //var car = _carRepository.GetCarById(addOrder.carId);
            //if(car == null)
            //{
            //    throw new Exception("Can't find car");
            //}
            
            //Todo: gửi vào email
            
            //Todo: tạo 1 order
            OrdersDTO ordersDTO = new OrdersDTO()
            {
                CarID = addOrder.carId
            };
        }
    }
}
