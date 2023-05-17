using DataAccess.DTO;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Util
{
    public class Mapper
    {
        public static UsersDTO mapToDTO(Users users)
        {
            if (users != null)
            {
                UsersDTO usersDTO = new UsersDTO
                {
                    UserID = users.UserID,
                    Name = users.Name,
                    Birthday = users.Birthday,
                    PhoneNumber = users.PhoneNumber,
                    EmailAddress = users.EmailAddress,
                    Password = users.Password,
                    RoleID = users.RoleID,
                    //roleName = users.RoleName,
                };
                return usersDTO;
            }
            else
            {
                return null;
            }

        }

        public static Users mapToEntity(UsersDTO usersDTO)
        {
            Users users = new Users
            {
                UserID = usersDTO.UserID,
                Name = usersDTO.Name,
                Birthday = usersDTO.Birthday,
                PhoneNumber = usersDTO.PhoneNumber,
                EmailAddress = usersDTO.EmailAddress,
                Password = usersDTO.Password,
                RoleID = usersDTO.RoleID,
                //RoleName = usersDTO.roleName,
            };

            return users;
        }

        public static RefreshTokenDTO mapToDTO(RefreshToken refreshToken)
        {
            if (refreshToken != null)
            {
                RefreshTokenDTO refreshTokenDTO = new RefreshTokenDTO
                {
                    TokenID = refreshToken.TokenID,
                    UserID = refreshToken.UserID,
                    Token = refreshToken.Token,
                    ExpiresDate = refreshToken.ExpiresDate,
                    CreateDate = refreshToken.CreateDate
                };
                return refreshTokenDTO;
            }
            else
            {
                return null;
            }
        }

        public static RefreshToken mapToEntity(RefreshTokenDTO refreshTokenDTO)
        {
            if (refreshTokenDTO != null)
            {
                RefreshToken refreshToken = new RefreshToken
                {
                    TokenID = refreshTokenDTO.TokenID,
                    UserID = refreshTokenDTO.UserID,
                    Token = refreshTokenDTO.Token,
                    ExpiresDate = refreshTokenDTO.ExpiresDate,
                    CreateDate = refreshTokenDTO.CreateDate
                };
                return refreshToken;
            }
            else
            {
                return null;
            }
        }

        public static CarDTO mapToDTO(Car cars)
        {
            if (cars != null)
            {
                CarDTO carDTO = new CarDTO
                {
                    CarID = cars.CarID,
                    Brand = cars.Brand,
                    Color = cars.Color,
                    LicensePlates = cars.LicensePlates,
                    Type = cars.Type,
                    UserID = cars.UserID
                };
                return carDTO;
            }
            else
            {
                return null;
            }

        }
        public static Car mapToEntity(CarDTO carDTO)
        {
            Car car = new Car
            {
                CarID = carDTO.CarID,
                Brand = carDTO.Brand,
                Color = carDTO.Color,
                LicensePlates = carDTO.LicensePlates,
                Type = carDTO.Type,
                UserID = carDTO.UserID,
            };

            return car;
        }

        public static OrdersDTO mapToDTO(Orders order)
        {
            OrdersDTO orderDTO = new OrdersDTO
            {
                OrderID = order.OrderID,
                CarID = order.CarID,
                GarageID = order.GarageID,
                TimeCreate = order.TimeCreate,
                TimeUpdate = order.TimeUpdate,
                Status = order.Status,
                //orderDetail = order.orderDetail,
            };
            return orderDTO;
        }

        public static Orders mapToEntity(OrdersDTO orderDTO)
        {
            Orders order = new Orders
            {
                OrderID = orderDTO.OrderID,
                CarID = orderDTO.CarID,
                GarageID = orderDTO.GarageID,
                TimeCreate = orderDTO.TimeCreate,
                TimeUpdate = orderDTO.TimeUpdate,
                Status = orderDTO.Status,
                OrderDetail = mapToEntity(orderDTO.OrderDetail)
            };
            return order;
        }

        public static OrderDetail mapToEntity(OrderDetailDTO orderDetailDTO)
        {
            OrderDetail orderDetail = new OrderDetail
            {
                OrderDetailID = orderDetailDTO.OrderDetailID,
                OrderID = orderDetailDTO.OrderID,
                ServiceID = orderDetailDTO.ServiceID,
                NameService = orderDetailDTO.NameService,
                Cost = orderDetailDTO.Cost,
                Note = orderDetailDTO.Note
            };

            return orderDetail;
        }
        public static OrderDetailDTO mapToDTO(OrderDetail orderDetail)
        {
            OrderDetailDTO orderDetailDTO = orderDetail == null ? null : new OrderDetailDTO
            {
                OrderDetailID = orderDetail.OrderDetailID,
                OrderID = orderDetail.OrderID,
                ServiceID = orderDetail.ServiceID,
                NameService = orderDetail.NameService,
                Cost = orderDetail.Cost,
                Note = orderDetail.Note
            };

            return orderDetailDTO;
        }

        public static Service mapToEntity(ServiceDTO serviceDTO)
        {
            Service service = new Service
            {
                ServiceID= serviceDTO.ServiceID,
                NameService = serviceDTO.NameService,
                GarageID = serviceDTO.GarageID,
                Cost = serviceDTO.Cost, 
                Note = serviceDTO.Note,
            };
            return service;
        }

        public static ServiceDTO mapToDTO(Service service)
        {
            ServiceDTO serviceDTO = new ServiceDTO
            {
                ServiceID = service.ServiceID,
                NameService = service.NameService,
                GarageID = service.GarageID,
                Cost = service.Cost,
                Note = service.Note,
            };
            return serviceDTO;
        }
    }
}

