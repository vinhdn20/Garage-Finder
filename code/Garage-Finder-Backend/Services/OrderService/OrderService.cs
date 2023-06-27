using DataAccess.DTO.Orders;
using DataAccess.DTO.Orders.RequestDTO;
using Repositories.Implements.Garage;
using Repositories.Interfaces;
using Services.PhoneVerifyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICarRepository _carRepository;
        private readonly ICategoryGarageRepository _categoryGarageRepository;
        private readonly IPhoneVerifyService _phoneVerifyService;
        private readonly IGuestOrderRepository _guestOrderRepository;

        public OrderService(IOrderRepository orderRepository, ICarRepository carRepository,
            ICategoryGarageRepository categoryGarageRepository, IPhoneVerifyService phoneVerifyService,
            IGuestOrderRepository guestOrderRepository)
        {
            _orderRepository = orderRepository;
            _carRepository = carRepository;
            _categoryGarageRepository = categoryGarageRepository;
            _phoneVerifyService = phoneVerifyService;
            _guestOrderRepository = guestOrderRepository;
        }

        public void AddOrderWithCar(AddOrderWithCarDTO addOrder)
        {
            // Todo: validate
            if (!_phoneVerifyService.VerifyPhoneNumber(addOrder.VerificationCode, addOrder.PhoneNumber).Result)
            {
                throw new Exception("Verification code not correct");
            }
            var car = _carRepository.GetCarById(addOrder.carId);
            if(car == null)
            {
                throw new Exception("Can't find car");
            }
            var category = _categoryGarageRepository.GetById(addOrder.categorygarageId);
            if(category is null)
            {
                throw new Exception("Can't find category");
            }
            //Todo: tạo 1 order
            OrdersDTO ordersDTO = new OrdersDTO()
            {
                CarID = addOrder.carId,
                GarageID = addOrder.garageId,
                CategoryGarageId = addOrder.categorygarageId,
                TimeCreate = DateTime.UtcNow,
                Status = Constants.STATUS_ORDER_OPEN,
                TimeAppointment = addOrder.TimeAppointment
            };

            _orderRepository.Add(ordersDTO);
            //Todo: gửi vào email

        }

        public void AddOrderFromGuest(AddOrderFromGuestDTO addOrder)
        {
            if (!_phoneVerifyService.VerifyPhoneNumber(addOrder.VerificationCode, addOrder.PhoneNumber).Result)
            {
                throw new Exception("Verification code not correct");
            }

            var category = _categoryGarageRepository.GetById(addOrder.CategoryGargeId);
            if (category is null)
            {
                throw new Exception("Can't find category");
            }

            GuestOrderDTO guestOrder = new GuestOrderDTO()
            {
                GarageID = addOrder.GarageId,
                CategoryGarageID = addOrder.CategoryGargeId,
                TimeCreate = DateTime.UtcNow,
                Status = Constants.STATUS_ORDER_OPEN,
                TimeAppointment = addOrder.TimeAppointment,
                PhoneNumber = addOrder.PhoneNumber,
                LicensePlates = addOrder.LicensePlates,
                Email = addOrder.Email,
                BrandCarID = addOrder.BrandCarID,
                TypeCar = addOrder.TypeCar
            };
            
            _guestOrderRepository.Add(guestOrder);
            //Todo: gửi vào email

        }
    }
}
