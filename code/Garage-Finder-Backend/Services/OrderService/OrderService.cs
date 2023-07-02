using AutoMapper;
using DataAccess.DTO;
using DataAccess.DTO.Orders;
using DataAccess.DTO.Orders.RequestDTO;
using DataAccess.DTO.Orders.ResponseDTO;
using GFData.Models.Entity;
using Mailjet.Client.Resources;
using Repositories.Implements.Garage;
using Repositories.Implements.OrderRepository;
using Repositories.Implements.UserRepository;
using Repositories.Interfaces;
using Services.EmailService;
using Services.PhoneVerifyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderService
{
    public class OrderService : IOrderService
    {
        #region variable
        private readonly IOrderRepository _orderRepository;
        private readonly ICarRepository _carRepository;
        private readonly ICategoryGarageRepository _categoryGarageRepository;
        private readonly IPhoneVerifyService _phoneVerifyService;
        private readonly IGuestOrderRepository _guestOrderRepository;
        private readonly IEmailService _emailService;
        private readonly IUsersRepository _usersRepository;
        private readonly IGarageRepository _garageRepository;
        private readonly IMapper _mapper;
        #endregion

        public OrderService(IOrderRepository orderRepository, ICarRepository carRepository,
            ICategoryGarageRepository categoryGarageRepository, IPhoneVerifyService phoneVerifyService,
            IGuestOrderRepository guestOrderRepository, IEmailService emailService, IUsersRepository usersRepository,
            IGarageRepository garageRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _carRepository = carRepository;
            _categoryGarageRepository = categoryGarageRepository;
            _phoneVerifyService = phoneVerifyService;
            _guestOrderRepository = guestOrderRepository;
            _emailService = emailService;
            _usersRepository = usersRepository;
            _garageRepository = garageRepository;
            _mapper = mapper;
        }

        public OrderDetailDTO GetOrderByGFID(int gfid, int userId)
        {
            OrderDetailDTO orderDetailDTO = null;
            var orders = _orderRepository.GetOrderByGFId(gfid);
            if(orders != null)
            {
                var car = _carRepository.GetCarById(orders.CarID);
                if(!ValidationGarageOwner(orders, userId) && !ValidationUserOwner(userId, orders))
                {
                    throw new Exception("Authorize exception!");
                }
                var user = _usersRepository.GetUserByID(car.UserID);
                orderDetailDTO = _mapper.Map<OrdersDTO, OrderDetailDTO>(orders);
                orderDetailDTO = _mapper.Map<CarDTO,OrderDetailDTO>(car, orderDetailDTO);
                orderDetailDTO = _mapper.Map(user, orderDetailDTO);
                orderDetailDTO.FileOrders = orders.FileOrders.Select(x => x.FileLink).ToList();
                orderDetailDTO.ImageOrders = orders.ImageOrders.Select(x => x.ImageLink).ToList();
            }
            else
            {
                var gorders = _guestOrderRepository.GetOrderByGFId(gfid);
                if(gorders == null)
                {
                    throw new Exception($"Can not found the order with id {gfid}");
                }
                if (!ValidationGarageOwner(gorders, userId))
                {
                    throw new Exception("Authorize exception!");
                }
                orderDetailDTO = _mapper.Map<GuestOrderDTO, OrderDetailDTO>(gorders);
                orderDetailDTO.FileOrders = gorders.FileOrders.Select(x => x.FileLink).ToList();
                orderDetailDTO.ImageOrders = gorders.ImageOrders.Select(x => x.ImageLink).ToList();
            }
            return orderDetailDTO;
            
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
            addOrder.categoryGarageId.ForEach(x => CheckCategoryExits(x));
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var cate in addOrder.categoryGarageId)
            {
                orderDetails.Add(new OrderDetail()
                {
                    CategoryGarageID = cate
                });
            }
            //Todo: tạo 1 order
            OrdersDTO ordersDTO = new OrdersDTO()
            {
                CarID = addOrder.carId,
                GarageID = addOrder.garageId,
                TimeCreate = DateTime.UtcNow,
                Status = Constants.STATUS_ORDER_OPEN,
                TimeAppointment = addOrder.TimeAppointment,
                OrderDetails = orderDetails
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

            addOrder.CategoryGargeId.ForEach(x => CheckCategoryExits(x));
            List<GuestOrderDetail> orderDetails = new List<GuestOrderDetail>();
            foreach (var cate in addOrder.CategoryGargeId)
            {
                orderDetails.Add(new GuestOrderDetail()
                {
                    CategoryGarageID = cate
                });
            }
            GuestOrderDTO guestOrder = new GuestOrderDTO()
            {
                GarageID = addOrder.GarageId,
                TimeCreate = DateTime.UtcNow,
                Status = Constants.STATUS_ORDER_OPEN,
                TimeAppointment = addOrder.TimeAppointment,
                PhoneNumber = addOrder.PhoneNumber,
                LicensePlates = addOrder.LicensePlates,
                Email = addOrder.Email,
                BrandCarID = addOrder.BrandCarID,
                TypeCar = addOrder.TypeCar,
                GuestOrderDetails = orderDetails
            };

            _guestOrderRepository.Add(guestOrder);
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

            addOrder.CategoryGargeId.ForEach(x => CheckCategoryExits(x));
            CarDTO carDTO = new CarDTO()
            {
                BrandID = addOrder.BrandCarID,
                LicensePlates = addOrder.LicensePlates,
                TypeCar = addOrder.TypeCar,
                UserID = userID
            };
            var car = _carRepository.SaveCar(carDTO); 
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var cate in addOrder.CategoryGargeId)
            {
                orderDetails.Add(new OrderDetail()
                {
                    CategoryGarageID = cate
                });
            }
            //Todo: tạo 1 order
            OrdersDTO ordersDTO = new OrdersDTO()
            {
                CarID = car.CarID,
                GarageID = addOrder.GarageId,
                TimeCreate = DateTime.UtcNow,
                Status = Constants.STATUS_ORDER_OPEN,
                TimeAppointment = addOrder.TimeAppointment,
                OrderDetails = orderDetails
            };

            _orderRepository.Add(ordersDTO);
            //Todo: gửi vào email
            var user = _usersRepository.GetUserByID(car.UserID);
            _emailService.SendMailAsync(user.EmailAddress, user.Name, "Your order create success!",
                $"<h3>Dear {user.Name}, Your order create success!</h3>");
        }
        #region Order
        public void GarageAcceptOrder(int GFId, int userId)
        {
            var order = _orderRepository.GetOrderByGFId(GFId);
            if(order != null)
            {
                if (!CheckGarageCanAcceptOrReject(userId, order))
                {
                    throw new Exception("Can not accept the order");
                }

                order.Status = Constants.STATUS_ORDER_CONFIRMED;
                order.TimeUpdate = DateTime.UtcNow;
                _orderRepository.Update(order);
            }
            else
            {
                var gorder = _guestOrderRepository.GetOrderByGFId(GFId);
                if (!CheckGarageCanAcceptOrReject(userId, gorder))
                {
                    throw new Exception("Can not accept the order");
                }
                if (gorder == null)
                {
                    throw new Exception("Can not find order");
                }
                gorder.Status = Constants.STATUS_ORDER_CONFIRMED;
                gorder.TimeUpdate = DateTime.UtcNow;
                _guestOrderRepository.Update(gorder);
            }
        }

        public void GarageRejectOrder(int GFId, int userId)
        {
            var order = _orderRepository.GetOrderByGFId(GFId);
            if(order != null)
            {
                if (!CheckGarageCanAcceptOrReject(userId, order))
                {
                    throw new Exception("Can not reject the order");
                }

                order.Status = Constants.STATUS_ORDER_REJECT;
                order.TimeUpdate = DateTime.UtcNow;
                _orderRepository.Update(order);
            }
            else
            {
                var gorder = _guestOrderRepository.GetOrderByGFId(GFId);
                if (!CheckGarageCanAcceptOrReject(userId, gorder))
                {
                    throw new Exception("Can not reject the order");
                }
                if (gorder == null)
                {
                    throw new Exception("Can not find order");
                }
                gorder.Status = Constants.STATUS_ORDER_REJECT;
                gorder.TimeUpdate = DateTime.UtcNow;
                _guestOrderRepository.Update(gorder);
            }
        }

        public void GarageCancelOrder(int GFId, int userId)
        {
            var order = _orderRepository.GetOrderByGFId(GFId);
            if (order != null)
            {
                if (!CheckGarageCanCancelOrDone(userId, order))
                {
                    throw new Exception("Can not cancel the order");
                }
                order.Status = Constants.STATUS_ORDER_CANCELED;
                order.TimeUpdate = DateTime.UtcNow;
                _orderRepository.Update(order);
            }
            else
            {
                var gorder = _guestOrderRepository.GetOrderByGFId(GFId);
                if (!CheckGarageCanAcceptOrReject(userId, gorder))
                {
                    throw new Exception("Can not cancel the order");
                }
                if (gorder == null)
                {
                    throw new Exception("Can not find order");
                }
                gorder.Status = Constants.STATUS_ORDER_CANCELED;
                gorder.TimeUpdate = DateTime.UtcNow;
                _guestOrderRepository.Update(gorder);
            }
        }

        public void GarageDoneOrder(DoneOrderDTO doneOrder, int userId)
        {
            var order = _orderRepository.GetOrderByGFId(doneOrder.GFOrderId);
            if(order != null)
            {
                if (!CheckGarageCanCancelOrDone(userId, order))
                {
                    throw new Exception("Can not done the order");
                }

                order.Status = Constants.STATUS_ORDER_DONE;
                order.TimeUpdate = DateTime.UtcNow;
                order.Content = doneOrder.Content;
                foreach (var image in doneOrder.ImageLinks)
                {
                    ImageOrdersDTO img = new ImageOrdersDTO()
                    {
                        ImageLink = image,
                        OrderID = order.OrderID,
                    };
                    order.ImageOrders.Add(img);
                }

                foreach(var file in doneOrder.FileLinks)
                {
                    FileOrdersDTO fileOrdersDTO = new FileOrdersDTO()
                    {
                        FileLink = file,
                        OrderID = order.OrderID,
                    };
                    order.FileOrders.Add(fileOrdersDTO);
                }
                _orderRepository.Update(order);
            }
            else
            {
                var gorder = _guestOrderRepository.GetOrderByGFId(doneOrder.GFOrderId);
                if (!CheckGarageCanCancelOrDone(userId, gorder))
                {
                    throw new Exception("Can not done the order");
                }
                if (gorder == null)
                {
                    throw new Exception("Can not find order");
                }
                gorder.Status = Constants.STATUS_ORDER_DONE;
                gorder.TimeUpdate = DateTime.UtcNow;
                gorder.Content = doneOrder.Content;
                foreach (var image in doneOrder.ImageLinks)
                {
                    ImageGuestOrder img = new ()
                    {
                        ImageLink = image,
                        GuestOrderID = gorder.GuestOrderID,
                    };
                    gorder.ImageOrders.Add(img);
                }

                foreach (var file in doneOrder.FileLinks)
                {
                    FileGuestOrders fileOrdersDTO = new ()
                    {
                        FileLink = file,
                        GuestOrderID = gorder.GuestOrderID,
                    };
                    gorder.FileOrders.Add(fileOrdersDTO);
                }
                _guestOrderRepository.Update(gorder);
            }
        }

        public void UserCancelOrder(int userId, int GFId)
        {
            var order = _orderRepository.GetOrderByGFId(GFId);
            if(order != null)
            {
                if (!CheckUserCanCancel(userId, order))
                {
                    throw new Exception("Can not cancel the order");
                }

                order.Status = Constants.STATUS_ORDER_CANCELED;
                order.TimeUpdate = DateTime.UtcNow;
                _orderRepository.Update(order);
            }
            else
            {
                var gorder = _guestOrderRepository.GetOrderByGFId(GFId);
                if (!CheckGarageCanAcceptOrReject(userId, gorder))
                {
                    throw new Exception("Can not cancel the order");
                }
                if (gorder == null)
                {
                    throw new Exception("Can not find order");
                }
                gorder.Status = Constants.STATUS_ORDER_CANCELED;
                gorder.TimeUpdate = DateTime.UtcNow;
                _guestOrderRepository.Update(gorder);
            }
        }

        #endregion
        #region Private Order method
        private bool CheckUserCanCancel(int userId, OrdersDTO orders)
        {
            if (!ValidationUserOwner(userId, orders))
            {
                return false;
            }

            if (orders.Status.Equals(Constants.STATUS_ORDER_OPEN) || orders.Status.Equals(Constants.STATUS_ORDER_CONFIRMED))
            {
                return true;
            }

            return false;
        }

        private bool CheckGarageCanAcceptOrReject(int userId, OrdersDTO orders)
        {
            if (!ValidationGarageOwner(orders, userId))
            {
                return false;
            }

            if (orders.Status.Equals(Constants.STATUS_ORDER_OPEN))
            {
                return true;
            }
            return false;
        }

        private bool CheckGarageCanCancelOrDone(int userId, OrdersDTO orders)
        {
            if (!ValidationGarageOwner(orders, userId))
            {
                return false;
            }

            if (orders.Status.Equals(Constants.STATUS_ORDER_CONFIRMED))
            {
                return true;
            }
            return false;
        }

        private bool ValidationGarageOwner(OrdersDTO orders, int userId)
        {
            var garages = _garageRepository.GetGarageByUser(userId);
            if (!garages.Any(x => x.GarageID == orders.GarageID))
            {
                return false;
            }
            return true;
        }

        private bool ValidationUserOwner(int userId, OrdersDTO orders)
        {
            var cars = _carRepository.GetCarsByUser(userId);
            if (!cars.Any(x => x.CarID == orders.CarID))
            {
                return false;
            }
            return true;
        }

        #endregion

        #region GuestOrder
        public void GarageAcceptGuestOrder(int orderId, int userId)
        {
            var order = _guestOrderRepository.GetOrderById(orderId);
            if (!CheckGarageCanAcceptOrReject(userId, order))
            {
                throw new Exception("Can not accept the order");
            }

            order.Status = Constants.STATUS_ORDER_CONFIRMED;
            order.TimeUpdate = DateTime.UtcNow;
            _guestOrderRepository.Update(order);
        }

        public void GarageRejectGuestOrder(int orderId, int userId)
        {
            var order = _guestOrderRepository.GetOrderById(orderId);
            if (!CheckGarageCanAcceptOrReject(userId, order))
            {
                throw new Exception("Can not reject the order");
            }

            order.Status = Constants.STATUS_ORDER_REJECT;
            order.TimeUpdate = DateTime.UtcNow;
            _guestOrderRepository.Update(order);
        }

        public void GarageCancelGuestOrder(int orderId, int userId)
        {
            var order = _guestOrderRepository.GetOrderById(orderId);
            if (!CheckGarageCanCancelOrDone(userId, order))
            {
                throw new Exception("Can not cancel the order");
            }
            order.Status = Constants.STATUS_ORDER_CANCELED;
            order.TimeUpdate = DateTime.UtcNow;
            _guestOrderRepository.Update(order);
        }

        public void GarageDoneGuestOrder(int orderId, int userId)
        {
            var order = _guestOrderRepository.GetOrderById(orderId);
            if (!CheckGarageCanCancelOrDone(userId, order))
            {
                throw new Exception("Can not done the order");
            }

            order.Status = Constants.STATUS_ORDER_DONE;
            order.TimeUpdate = DateTime.UtcNow;
            _guestOrderRepository.Update(order);
        }
        #endregion
        #region Private Guest Order method

        private bool CheckGarageCanAcceptOrReject(int userId, GuestOrderDTO orders)
        {
            if (!ValidationGarageOwner(orders, userId))
            {
                return false;
            }

            if (orders.Status.Equals(Constants.STATUS_ORDER_OPEN))
            {
                return true;
            }
            return false;
        }

        private bool CheckGarageCanCancelOrDone(int userId, GuestOrderDTO orders)
        {
            if (!ValidationGarageOwner(orders, userId))
            {
                return false;
            }

            if (orders.Status.Equals(Constants.STATUS_ORDER_CONFIRMED))
            {
                return true;
            }
            return false;
        }

        private bool ValidationGarageOwner(GuestOrderDTO orders, int userId)
        {
            var garages = _garageRepository.GetGarageByUser(userId);
            if (!garages.Any(x => x.GarageID == orders.GarageID))
            {
                return false;
            }
            return true;
        }

        private void CheckCategoryExits(int id)
        {
            var category = _categoryGarageRepository.GetById(id);
            if (category is null)
            {
                throw new Exception("Can't find category");
            }
        }
        #endregion
    }
}
