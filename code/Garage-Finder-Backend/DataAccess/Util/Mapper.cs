using DataAccess.DAO;
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
                    Status = users.Status,
                    LinkImage = users.LinkImage
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
                Status = usersDTO.Status,
                LinkImage = usersDTO.LinkImage
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

        public static CarDTO mapToDTO(Car car)
        {
            if (car != null)
            {
                CarDTO carDTO = new CarDTO
                {
                    CarID = car.CarID,
                    BrandID = car.BrandID,
                    Color = car.Color,
                    LicensePlates = car.LicensePlates,
                    TypeCar = car.TypeCar,
                    UserID = car.UserID,
                    LinkImages = car.LinkImages,
                };
                return carDTO;
            }
            else
            {
                return null;
            }
        }

        public static RoleNameDTO mapToDTO(RoleName roleName)
        {
            if (roleName != null)
            {
                RoleNameDTO roleNameDTO = new RoleNameDTO
                {
                    RoleID = roleName.RoleID,
                    NameRole = roleName.NameRole,
                };
                return roleNameDTO;
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
                BrandID = carDTO.BrandID,
                Color = carDTO.Color,
                LicensePlates = carDTO.LicensePlates,
                TypeCar = carDTO.TypeCar,
                UserID = carDTO.UserID,
                LinkImages = carDTO.LinkImages,
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
                ServiceID = order.ServiceID,
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
                ServiceID = orderDTO.ServiceID,
                TimeCreate = orderDTO.TimeCreate,
                TimeUpdate = orderDTO.TimeUpdate,
                Status = orderDTO.Status,
                //OrderDetail = mapToEntity(orderDTO.OrderDetail);
            };
            return order;
        }

        public static Service mapToEntity(ServiceDTO serviceDTO)
        {
            Service service = new Service
            {
                ServiceID = serviceDTO.ServiceID,
                NameService = serviceDTO.NameService,
                GarageID = serviceDTO.GarageID,
                //Cost = serviceDTO.Cost,
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
                //Cost = service.Cost,
                Note = service.Note,
            };
            return serviceDTO;
        }

        public static Categorys mapToEntity(CategoryDTO categoryDTO)
        {
            Categorys category = new Categorys
            {
                CategoryID = categoryDTO.CategoryID,
                CategoryName = categoryDTO.CategoryName
            };
            return category;
        }
        public static CategoryDTO mapToDTO(Categorys category)
        {
            CategoryDTO categoryDTO = new CategoryDTO
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName
            };
            return categoryDTO;
        }

        public static GarageDTO mapToDTO(Garage garage)
        {
            if (garage != null)
            {
                GarageDTO garageDTO = new GarageDTO
                {
                    GarageID=garage.GarageID,
                    GarageName=garage.GarageName,
                    Address = garage.Address,
                    EmailAddress = garage.EmailAddress,
                    UserID = garage.UserID,
                    PhoneNumber = garage.PhoneNumber,
                    Status = garage.Status,
                    //OpenTime = garage.OpenTime,
                    Logo = garage.Logo,
                    Imagies = garage.Imagies,
                    //Location = garage.Location,
                };
                return garageDTO;
            }
            else
            {
                return null;
            }

        }

        public static Garage mapToEntity(GarageDTO garageDTO)
        {
            Garage garage = new Garage
            {
                GarageID = garageDTO.GarageID,
                GarageName = garageDTO.GarageName,
                Address = garageDTO.Address,
                EmailAddress = garageDTO.EmailAddress,
                UserID = garageDTO.UserID,
                PhoneNumber = garageDTO.PhoneNumber,
                Status = garageDTO.Status,
                //OpenTime = garageDTO.OpenTime,
                Logo = garageDTO.Logo,
                Imagies = garageDTO.Imagies,
                //Location = garageDTO.Location,
            };

            return garage;
        }
        public static FavoriteList mapToEntity(FavoriteListDTO favoriteListDTO)
        {
            FavoriteList favoriteList = new FavoriteList
            {
                FavoriteID = favoriteListDTO.FavoriteID,
                UserID = favoriteListDTO.UserID,
                GarageID = favoriteListDTO.GarageID,

            };

            return favoriteList;
        }

        public static FavoriteListDTO mapToDTO(FavoriteList favoriteList)
        {
            if (favoriteList != null)
            {
                FavoriteListDTO favoriteListDTO = new FavoriteListDTO
                {
                    FavoriteID = favoriteList.FavoriteID,
                    UserID = favoriteList.UserID,
                    GarageID = favoriteList.GarageID,
                };
                return favoriteListDTO;
            }
            else
            {
                return null;
            }

        }

        public static FeedbackDTO mapToDTO(Feedback feedback)
        {
            if (feedback != null)
            {
                FeedbackDTO feedbackDTO = new FeedbackDTO
                {
                    FeedbackID = feedback.FeedbackID,
                    GarageID = feedback.GarageID,
                    UserID = feedback.UserID,
                    Star = feedback.Star,
                    Content = feedback.Content,
                };
                return feedbackDTO;
            }
            else
            {
                return null;
            }

        }

        public static Feedback mapToEntity(FeedbackDTO feedbackDTO)
        {
            Feedback feedback = new Feedback
            {
                FeedbackID = feedbackDTO.FeedbackID,
                GarageID = feedbackDTO.GarageID,
                UserID = feedbackDTO.UserID,
                Star  =  feedbackDTO.Star,
                Content  = feedbackDTO.Content,
            };

            return feedback;
        }

        public static SubscribeDTO mapToDTO(Subscribe subscribe)
        {
            if (subscribe != null)
            {
                SubscribeDTO subscribeDTO = new SubscribeDTO
                {
                    SubscribeID = subscribe.SubscribeID,
                    Content = subscribe.Content,
                    Price = subscribe.Price,
                    Period = subscribe.Period
                };
                return subscribeDTO;
            }
            else
            {
                return null;
            }
        }

        public static Subscribe mapToEntity(SubscribeDTO subscribeDTO)
        {
            if (subscribeDTO != null)
            {
                Subscribe subscribe = new Subscribe
                {
                    SubscribeID = subscribeDTO.SubscribeID,
                    Content = subscribeDTO.Content,
                    Price = subscribeDTO.Price,
                    Period = subscribeDTO.Period
                };
                return subscribe;
            }
            else
            {
                return null;
            }
        }
    }
}