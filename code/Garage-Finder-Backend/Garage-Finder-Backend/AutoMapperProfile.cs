using AutoMapper;
using DataAccess.DTO;
using DataAccess.DTO.Orders;
using DataAccess.DTO.RequestDTO.Car;
using DataAccess.DTO.RequestDTO.Garage;
using DataAccess.DTO.User.ResponeModels;
using GFData.Models.Entity;

namespace Garage_Finder_Backend
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BrandDTO, Brand>();
            CreateMap<Brand, BrandDTO>();
            CreateMap<CarDTO, Car>();
            CreateMap<Car, CarDTO>();
            CreateMap<CategoryDTO, Categorys>(); 
            CreateMap<Categorys, CategoryDTO>();
            CreateMap<FavoriteListDTO, FavoriteList>();
            CreateMap<FavoriteList, FavoriteListDTO>();
            CreateMap<FeedbackDTO, Feedback>();
            CreateMap<Feedback, FeedbackDTO>();
            CreateMap<GarageDTO, Garage>();
            CreateMap<Garage, GarageDTO>();
            CreateMap<GarageBrandDTO, GarageBrand>();
            CreateMap<GarageBrand, GarageBrandDTO>();
            CreateMap<GarageInfoDTO, GarageInfo>();
            CreateMap<GarageInfo, GarageInfoDTO>();
            CreateMap<InvoicesDTO, Invoices>();
            CreateMap<Invoices, InvoicesDTO>();
            CreateMap<NotificationDTO, Notification>();
            CreateMap<Notification, NotificationDTO>();

            CreateMap<OrdersDTO, Orders>();
            CreateMap<Orders, OrdersDTO>();
            CreateMap<GuestOrderDTO, GuestOrder>();
            CreateMap<GuestOrder, GuestOrderDTO>();
            CreateMap<RefreshTokenDTO, RefreshToken>();
            CreateMap<RefreshToken, RefreshTokenDTO>();
            CreateMap<RoleNameDTO, RoleName>();
            CreateMap<RoleName, RoleNameDTO>();
            CreateMap<ServiceDTO, Service>();
            CreateMap<Service, ServiceDTO>();
            CreateMap<SubscribeDTO, Subscribe>();
            CreateMap<Subscribe, SubscribeDTO>();

            CreateMap<UsersDTO, Users>();
            CreateMap<Users, UsersDTO>();
            CreateMap<UsersDTO, UserInfor>();
            CreateMap<UserInfor,UsersDTO>();
            CreateMap<Users, UserInfor>();

            CreateMap<FileOrders, FileOrdersDTO>();
            CreateMap<FileOrdersDTO, FileOrders>();
            CreateMap<ImageOrders, ImageOrdersDTO>();
            CreateMap<ImageOrdersDTO, ImageOrders>();

            CreateMap<GarageDTO, AddGarageDTO>();
            CreateMap<AddGarageDTO, GarageDTO>();

            CreateMap<CategoryGarage, CategoryGarageDTO>();
            CreateMap<CategoryGarageDTO, CategoryGarage>();

            CreateMap<ImageGarageDTO, ImageGarage>();
            CreateMap<ImageGarage, ImageGarageDTO>();

            CreateMap<UpdateGarageDTO, Garage>();

            CreateMap<AddCarDTO, CarDTO>();
            CreateMap<UpdateCarDTO, CarDTO>();
        }
    }
}
