using DataAccess.DTO;
using DataAccess.DTO.Orders;
using DataAccess.DTO.Orders.RequestDTO;
using Mailjet.Client.Resources;
using Repositories.Implements.Garage;
using Repositories.Implements.UserRepository;
using Repositories.Interfaces;
using Services.EmailService;
using Services.PhoneVerifyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
        private readonly IEmailService _emailService;
        private readonly IUsersRepository _usersRepository;

        public OrderService(IOrderRepository orderRepository, ICarRepository carRepository,
            ICategoryGarageRepository categoryGarageRepository, IPhoneVerifyService phoneVerifyService,
            IGuestOrderRepository guestOrderRepository, IEmailService emailService, IUsersRepository usersRepository)
        {
            _orderRepository = orderRepository;
            _carRepository = carRepository;
            _categoryGarageRepository = categoryGarageRepository;
            _phoneVerifyService = phoneVerifyService;
            _guestOrderRepository = guestOrderRepository;
            _emailService = emailService;
            _usersRepository = usersRepository;
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
            var user = _usersRepository.GetUserByID(car.UserID);
            _emailService.SendMailAsync(user.EmailAddress, user.Name, "Your order create success!",
                $"<h3>Dear {user.Name}, Your order create success!</h3>");
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

            //_guestOrderRepository.Add(guestOrder);
            //Todo: gửi vào email
            _emailService.SendMailAsync(addOrder.Email, addOrder.Name, "Your order create success!",
                $"<h3>Dear {addOrder.Name}, Your order create success!</h3>");
        }

        public void AddOrderWithoutCar(AddOrderWithoutCarDTO addOrder, int userID)
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
            CarDTO carDTO = new CarDTO()
            {
                BrandID = addOrder.BrandCarID,
                LicensePlates = addOrder.LicensePlates,
                TypeCar = addOrder.TypeCar,
                UserID = userID
            };
            var car = _carRepository.SaveCar(carDTO);
            //Todo: tạo 1 order
            OrdersDTO ordersDTO = new OrdersDTO()
            {
                CarID = car.CarID,
                GarageID = addOrder.GarageId,
                CategoryGarageId = addOrder.CategoryGargeId,
                TimeCreate = DateTime.UtcNow,
                Status = Constants.STATUS_ORDER_OPEN,
                TimeAppointment = addOrder.TimeAppointment
            };

            _orderRepository.Add(ordersDTO);
            //Todo: gửi vào email
            var user = _usersRepository.GetUserByID(car.UserID);
            _emailService.SendMailAsync(user.EmailAddress, user.Name, "Your order create success!",
                $"<h3>Dear {user.Name}, Your order create success!</h3>");
        }
    }
}
