using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO.Car;
using DataAccess.DTO.Orders;
using DataAccess.DTO.Orders.RequestDTO;
using DataAccess.DTO.Orders.ResponseDTO;
using GFData.Models.Entity;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.SignalR;
using Repositories.Interfaces;
using Services.EmailService;
using Services.GarageService;
using Services.NotificationService;
using Services.PhoneVerifyService;
using Services.WebSocket;
using System.Runtime.InteropServices;

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
        private readonly IGarageService _garageService;
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly INotificationService _notificationService;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;
        #endregion

        public OrderService(IOrderRepository orderRepository, ICarRepository carRepository,
            ICategoryGarageRepository categoryGarageRepository, IPhoneVerifyService phoneVerifyService,
            IGuestOrderRepository guestOrderRepository, IEmailService emailService, IUsersRepository usersRepository,
            IGarageRepository garageRepository,IBrandRepository brandRepository, IMapper mapper,
            ICategoryRepository categoryRepository, IStaffRepository staffRepository,
            INotificationService notificationService, IFeedbackRepository feedbackRepository,
            IGarageService garageService)
        {
            _orderRepository = orderRepository;
            _carRepository = carRepository;
            _categoryGarageRepository = categoryGarageRepository;
            _phoneVerifyService = phoneVerifyService;
            _guestOrderRepository = guestOrderRepository;
            _emailService = emailService;
            _usersRepository = usersRepository;
            _garageRepository = garageRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _staffRepository = staffRepository;
            _notificationService = notificationService;
            _feedbackRepository = feedbackRepository;
            _garageService = garageService;
        }

        public List<OrderDetailDTO> GetByUserId(int userId)
        {
            var orders = _orderRepository.GetAllOrdersByUserId(userId);
            var feedbacks = _feedbackRepository.GetListByUserId(userId);
            List<OrderDetailDTO> list = new List<OrderDetailDTO>();
            foreach (var ord in orders)
            {
                var o = _mapper.Map<OrderDetailDTO>(ord);
                var car = _carRepository.GetCarById(ord.CarID);
                var userDB = _usersRepository.GetUserByID(car.UserID);
                o = _mapper.Map(car, o); 
                var brand = _brandRepository.GetBrand().FirstOrDefault(x => x.BrandID == car.BrandID);
                o.Brand = brand.BrandName;

                o = _mapper.Map(userDB, o);
                o.FileOrders = ord.FileOrders.Select(x => x.FileLink).ToList();
                o.ImageOrders = ord.ImageOrders.Select(x => x.ImageLink).ToList();
                o.Name = userDB.Name;

                o.Category = new List<string>();
                foreach (var detail in ord.OrderDetails)
                {
                    var categoryGarage = _categoryGarageRepository.GetById(detail.CategoryGarageID);
                    var cate = _categoryRepository.GetCategory().Where(x => x.CategoryID == categoryGarage.CategoryID).FirstOrDefault();
                    o.Category.Add(cate.CategoryName);
                }
                if(feedbacks.Any(x => x.OrderID == o.OrderID))
                {
                    o.IsFeedback = true;
                }
                list.Add(o);
            }
            return list;
        }

        public OrderDetailDTO GetOrderByGFID(int gfid, int userId)
        {
            OrderDetailDTO orderDetailDTO = null;
            var orders = _orderRepository.GetOrderByGFId(gfid);
            if(orders != null)
            {
                var car = _carRepository.GetCarById(orders.CarID);
                if(!ValidationGarageOwner(orders, userId) && !ValidationUserOwner(userId, orders)
                    && !ValidationStaff(userId, orders))
                {
                    throw new Exception("Authorize exception!");
                }
                var user = _usersRepository.GetUserByID(car.UserID);
                orderDetailDTO = _mapper.Map<OrdersDTO, OrderDetailDTO>(orders);
                orderDetailDTO = _mapper.Map<CarDTO,OrderDetailDTO>(car, orderDetailDTO);
                orderDetailDTO = _mapper.Map(user, orderDetailDTO);
                orderDetailDTO.FileOrders = orders.FileOrders.Select(x => x.FileLink).ToList();
                orderDetailDTO.ImageOrders = orders.ImageOrders.Select(x => x.ImageLink).ToList();
                orderDetailDTO.Name = user.Name;
                var brand = _brandRepository.GetBrand().FirstOrDefault(x => x.BrandID == car.BrandID);
                orderDetailDTO.Brand = brand.BrandName;

                orderDetailDTO.IsFeedback = _feedbackRepository.GetAll().Any(x => x.OrderID == orders.OrderID);
            }
            else
            {
                var gorders = _guestOrderRepository.GetOrderByGFId(gfid);
                if(gorders == null)
                {
                    throw new Exception($"Can not found the order with id {gfid}");
                }
                if (!ValidationGarageOwner(gorders, userId) && !ValidationStaff(userId, orders))
                {
                    throw new Exception("Authorize exception!");
                }
                orderDetailDTO = _mapper.Map<GuestOrderDTO, OrderDetailDTO>(gorders);
                orderDetailDTO.FileOrders = gorders.FileOrders.Select(x => x.FileLink).ToList();
                orderDetailDTO.ImageOrders = gorders.ImageOrders.Select(x => x.ImageLink).ToList();
                orderDetailDTO.Name = gorders.Name;
                var brand = _brandRepository.GetBrand().FirstOrDefault(x => x.BrandID == gorders.BrandCarID);
                orderDetailDTO.Brand = brand.BrandName;
            }
            return orderDetailDTO;

        }

        public List<OrderDetailDTO> GetOrderByGarageId(int garageId, int userId)
        {
            var garagas = _garageRepository.GetGarageByUser(userId); 
            var staff = _staffRepository.GetByGarageId(garageId);

            var feedbacks = _feedbackRepository.GetListByGarage(garageId);
            if (!garagas.Any(x => x.GarageID == garageId) && !staff.Any(x => x.StaffId == userId))
            {
                throw new Exception("Authorize exception");
            }
            var orders = _orderRepository.GetAllOrdersByGarageId(garageId);
            var gorders = _guestOrderRepository.GetOrdersByGarageId(garageId);
            List<OrderDetailDTO> list = new List<OrderDetailDTO>();
            foreach (var ord in orders)
            {
                var o = _mapper.Map<OrderDetailDTO>(ord);
                var car = _carRepository.GetCarById(ord.CarID);
                var user = _usersRepository.GetUserByID(car.UserID);
                o = _mapper.Map<OrdersDTO, OrderDetailDTO>(ord);
                o = _mapper.Map(car, o);
                o = _mapper.Map(user, o);
                o.FileOrders = ord.FileOrders.Select(x => x.FileLink).ToList();
                o.ImageOrders = ord.ImageOrders.Select(x => x.ImageLink).ToList();
                o.Name = user.Name;
                var brand = _brandRepository.GetBrand().FirstOrDefault(x => x.BrandID == car.BrandID);
                o.Brand = brand.BrandName;

                o.Category = new List<string>();
                foreach (var detail in ord.OrderDetails)
                {
                    var categoryGarage = _categoryGarageRepository.GetById(detail.CategoryGarageID);
                    var cate = _categoryRepository.GetCategory().Where(x => x.CategoryID == categoryGarage.CategoryID).FirstOrDefault();
                    o.Category.Add(cate.CategoryName);
                }
                o.IsFeedback = feedbacks.Any(x => x.OrderID == ord.OrderID);
                list.Add(o);
            }

            foreach (var order in gorders)
            {
                var o = _mapper.Map<GuestOrderDTO, OrderDetailDTO>(order);
                o.FileOrders = order.FileOrders.Select(x => x.FileLink).ToList();
                o.ImageOrders = order.ImageOrders.Select(x => x.ImageLink).ToList();
                o.Name = order.Name;
                var brand = _brandRepository.GetBrand().FirstOrDefault(x => x.BrandID == order.BrandCarID);
                o.Brand = brand.BrandName;
                o.Category = new List<string>();
                foreach (var detail in order.GuestOrderDetails)
                {
                    var categoryGarage = _categoryGarageRepository.GetById(detail.CategoryGarageID);
                    var cate = _categoryRepository.GetCategory().Where(x => x.CategoryID == categoryGarage.CategoryID).FirstOrDefault();
                    o.Category.Add(cate.CategoryName);
                }
                list.Add(o);
            }
            return list;
        }

        public void AddOrderWithCar(AddOrderWithCarDTO addOrder)
        {
            if (!addOrder.PhoneNumber.IsValidPhone())
            {
                throw new Exception("Phone number is not valid");
            }

            if (!_phoneVerifyService.VerifyPhoneNumber(addOrder.VerificationCode, addOrder.PhoneNumber).Result)
            {
                throw new Exception("Verification code not correct");
            }
            var car = _carRepository.GetCarById(addOrder.carId);
            if(car == null)
            {
                throw new Exception("Can't find car");
            }
            var garage = _garageService.GetById(addOrder.garageId);
            var brandCar = _brandRepository.GetBrand().Find(x => x.BrandID == car.BrandID);
            if(!garage.GarageBrands.Any(x => x.BrandName.Equals(brandCar.BrandName)))
            {
                throw new Exception($"Garage này không hỗ trợ sửa xe {brandCar.BrandName}");
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
                TimeAppointment = DateTime.ParseExact(addOrder.TimeAppointment, "dd/MM/yyyy hh:mm tt",
                System.Globalization.CultureInfo.InvariantCulture),
                OrderDetails = orderDetails,
                PhoneNumber = addOrder.PhoneNumber,
            };

            _orderRepository.Add(ordersDTO);
            //Todo: gửi vào email
            var user = _usersRepository.GetUserByID(car.UserID);
            //_emailService.SendMailAsync(user.EmailAddress, user.Name, "Your order create success!",
            //    $"<h3>Dear {user.Name}, Your order create success!</h3>");

            _notificationService.SendNotificationToStaff(ordersDTO, user.UserID);
        }

        public void AddOrderFromGuest(AddOrderFromGuestDTO addOrder)
        {
            if (!addOrder.PhoneNumber.IsValidPhone())
            {
                throw new Exception("Phone number is not valid");
            }

            if (!addOrder.Email.IsValidEmail())
            {
                throw new Exception("Email is not valid");
            }
            if (!_phoneVerifyService.VerifyPhoneNumber(addOrder.VerificationCode, addOrder.PhoneNumber).Result)
            {
                throw new Exception("Verification code not correct");
            }
            var garage = _garageService.GetById(addOrder.GarageId);
            var brandCar = _brandRepository.GetBrand().Find(x => x.BrandID == addOrder.BrandCarID);
            if (!garage.GarageBrands.Any(x => x.BrandName.Equals(brandCar.BrandName)))
            {
                throw new Exception($"Garage này không hỗ trợ sửa xe {brandCar.BrandName}");
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
                TimeAppointment = DateTime.ParseExact(addOrder.TimeAppointment, "dd/MM/yyyy hh:mm tt",
                System.Globalization.CultureInfo.InvariantCulture),
                PhoneNumber = addOrder.PhoneNumber,
                LicensePlates = addOrder.LicensePlates,
                Email = addOrder.Email,
                BrandCarID = addOrder.BrandCarID,
                TypeCar = addOrder.TypeCar,
                GuestOrderDetails = orderDetails,
                Name = addOrder.Name
            };

            _guestOrderRepository.Add(guestOrder);
            //Todo: gửi vào email
            //_emailService.SendMailAsync(addOrder.Email, addOrder.Name, "Your order create success!",
            //    $"<h3>Dear {addOrder.Name}, Your order create success!</h3>");
            _notificationService.SendNotificationToStaff(guestOrder);
        }

        public void AddOrderWithoutCar(AddOrderWithoutCarDTO addOrder, int userID)
        {
            if (!addOrder.PhoneNumber.IsValidPhone())
            {
                throw new Exception("Phone number is not valid");
            }
            if (!_phoneVerifyService.VerifyPhoneNumber(addOrder.VerificationCode, addOrder.PhoneNumber).Result)
            {
                throw new Exception("Verification code not correct");
            }

            if (!addOrder.LicensePlates.IsValidLicensePlates())
            {
                throw new Exception("License Plates not valid");
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
            var garage = _garageService.GetById(addOrder.GarageId);
            var brandCar = _brandRepository.GetBrand().Find(x => x.BrandID == addOrder.BrandCarID);
            if (!garage.GarageBrands.Any(x => x.BrandName.Equals(brandCar.BrandName)))
            {
                throw new Exception($"Garage này không hỗ trợ sửa xe {brandCar.BrandName}");
            }
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
                TimeAppointment = DateTime.ParseExact(addOrder.TimeAppointment, "dd/MM/yyyy hh:mm tt",
                System.Globalization.CultureInfo.InvariantCulture),
                OrderDetails = orderDetails,
                PhoneNumber = addOrder.PhoneNumber,
            };

            _orderRepository.Add(ordersDTO);
            //Todo: gửi vào email
            var user = _usersRepository.GetUserByID(car.UserID);
            //_emailService.SendMailAsync(user.EmailAddress, user.Name, "Your order create success!",
            //    $"<h3>Dear {user.Name}, Your order create success!</h3>");
            _notificationService.SendNotificationToStaff(ordersDTO, user.UserID);
        }
        #region Order
        public void GarageAcceptOrder(int GFId, int userId)
        {
            var order = _orderRepository.GetOrderByGFId(GFId);
            if (order != null)
            {
                if (!CheckGarageCanAcceptOrReject(userId, order))
                {
                    throw new Exception("Can not accept the order");
                }

                order.Status = Constants.STATUS_ORDER_CONFIRMED;
                order.TimeUpdate = DateTime.UtcNow;
                _orderRepository.Update(order);
                _notificationService.SendNotificatioToUser(order, userId);

                var car = _carRepository.GetCarById(order.CarID);
                var user = _usersRepository.GetUserByID(car.UserID);
                var garage = _garageRepository.GetGaragesByID(order.GarageID);
                var categoryGarageId = order.OrderDetails.Select(x => x.CategoryGarageID).ToList();
                var categoryGarage = garage.CategoryGarages.Where(x => categoryGarageId.Any(c => c == x.CategoryGarageID)).ToList();
                var category = _categoryRepository.GetCategory().Where(x => categoryGarage.Any(c => c.CategoryID == x.CategoryID)).ToList();
                string categoryText = category.FirstOrDefault().CategoryName;
                for (int i = 1; i < category.Count(); i++)
                {
                    categoryText += "," + category[i].CategoryName;
                }
                _emailService.SendMailAsync(user.EmailAddress, user.Name, "Lịch đặt của bạn đã được xác nhận!",
                $"<h1>Lịch đặt của bạn đã được {garage.GarageName} xác nhận</h1><br/>" +
                $"Garage: {garage.GarageName}<br/>" +
                $"Loại dịch vụ: {categoryText}<br/>" +
                $"Thời gian: {order.TimeCreate}<br/>" +
                $"Địa điểm: {garage.AddressDetail}<br/>" +
                $"Quý khách vui lòng mang xe đến đúng thời gian đặt lịch<br/><br/>" +
                $"{garage.GarageName} chân thành cảm ơn quý khách !");
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
                var garage = _garageRepository.GetGaragesByID(gorder.GarageID);
                var categoryGarageId = order.OrderDetails.Select(x => x.CategoryGarageID).ToList();
                var categoryGarage = garage.CategoryGarages.Where(x => categoryGarageId.Any(c => c == x.CategoryGarageID)).ToList();
                var category = _categoryRepository.GetCategory().Where(x => categoryGarage.Any(c => c.CategoryID == x.CategoryID)).ToList();
                string categoryText = category.FirstOrDefault().CategoryName;
                for (int i = 1; i < category.Count(); i++)
                {
                    categoryText += "," + category[i].CategoryName;
                }
                _emailService.SendMailAsync(gorder.Email, gorder.Name, "Lịch đặt của bạn đã được xác nhận!",
                $"<h1>Lịch đặt của bạn đã được{garage.GarageName} xác nhận</h1><br/>" +
                $"Garage: {garage.GarageName}<br/>" +
                $"Loại dịch vụ: {categoryText}<br/>Thời gian: {gorder.TimeCreate}<br/>" +
                $"Địa điểm: {garage.AddressDetail}<br/>" +
                $"Quý khách vui lòng mang xe đến đúng thời gian đặt lịch<br/><br/>" +
                $"{garage.GarageName} chân thành cảm ơn quý khách !");
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
                _notificationService.SendNotificatioToUser(order, userId);

                var car = _carRepository.GetCarById(order.CarID);
                var user = _usersRepository.GetUserByID(car.UserID);
                var garage = _garageRepository.GetGaragesByID(order.GarageID);
                var categoryGarageId = order.OrderDetails.Select(x => x.CategoryGarageID).ToList();
                var categoryGarage = garage.CategoryGarages.Where(x => categoryGarageId.Any(c => c == x.CategoryGarageID)).ToList();
                var category = _categoryRepository.GetCategory().Where(x => categoryGarage.Any(c => c.CategoryID == x.CategoryID)).ToList();
                string categoryText = category.FirstOrDefault().CategoryName;
                for (int i = 1; i < category.Count(); i++)
                {
                    categoryText += "," + category[i].CategoryName;
                }
                _emailService.SendMailAsync(user.EmailAddress, user.Name, "Lịch đặt của bạn đã bị từ chối!",
                $"<h1>Lịch đặt của bạn đã bị {garage.GarageName} từ chối</h1><br/>" +
                $"Garage: {garage.GarageName}<br/>" +
                $"Loại dịch vụ: {categoryText}<br/>" +
                $"Thời gian: {order.TimeCreate}<br/>" +
                $"Địa điểm: {garage.AddressDetail}");
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

                var garage = _garageRepository.GetGaragesByID(gorder.GarageID);
                var categoryGarageId = order.OrderDetails.Select(x => x.CategoryGarageID).ToList();
                var categoryGarage = garage.CategoryGarages.Where(x => categoryGarageId.Any(c => c == x.CategoryGarageID)).ToList();
                var category = _categoryRepository.GetCategory().Where(x => categoryGarage.Any(c => c.CategoryID == x.CategoryID)).ToList();
                string categoryText = category.FirstOrDefault().CategoryName;
                for (int i = 1; i < category.Count(); i++)
                {
                    categoryText += "," + category[i].CategoryName;
                }
                _emailService.SendMailAsync(gorder.Email, gorder.Name, "Lịch đặt của bạn đã bị từ chối!",
                $"<h1>Lịch đặt của bạn đã bị {garage.GarageName} từ chối</h1><br/>" +
                $"Garage: {garage.GarageName}<br/>" +
                $"Loại dịch vụ: {categoryText}<br/>" +
                $"Thời gian: {gorder.TimeCreate}<br/>" +
                $"Địa điểm: {garage.AddressDetail}");
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
                _notificationService.SendNotificatioToUser(order, userId);
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
                //_emailService.SendMailAsync(gorder.Email, gorder.Name, "Your order create success!",
                //$"<h3>Dear {gorder.Name}, Your order create success!</h3>");
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
                _notificationService.SendNotificatioToUser(order, userId);
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
                //_emailService.SendMailAsync(gorder.Email, gorder.Name, "Your order create success!",
                //$"<h3>Dear {gorder.Name}, Your order create success!</h3>");
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
                _notificationService.SendNotificationToStaff(order, userId);
            }
            else
            {
                throw new Exception("Can not find order");
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
            if (!ValidationGarageOwner(orders, userId) && !ValidationStaff(userId, orders))
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
            if (!ValidationGarageOwner(orders, userId) && !ValidationStaff(userId, orders))
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

        private bool ValidationStaff(int userId, OrdersDTO orders)
        {
            var staff =_staffRepository.GetByGarageId(orders.GarageID);
            if(!staff.Any(x => x.StaffId == userId))
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
            if (!ValidationGarageOwner(orders, userId) && !ValidationStaff(userId, orders))
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
            if (!ValidationGarageOwner(orders, userId) && !ValidationStaff(userId, orders))
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

        private bool ValidationStaff(int userId, GuestOrderDTO orders)
        {
            var staff = _staffRepository.GetByGarageId(orders.GarageID);
            if (!staff.Any(x => x.StaffId == userId))
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
