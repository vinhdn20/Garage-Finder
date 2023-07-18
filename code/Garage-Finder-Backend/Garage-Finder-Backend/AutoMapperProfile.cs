using AutoMapper;
using DataAccess.DTO;
using DataAccess.DTO.Car;
using DataAccess.DTO.Category;
using DataAccess.DTO.Feedback;
using DataAccess.DTO.Garage;
using DataAccess.DTO.Orders;
using DataAccess.DTO.Orders.ResponseDTO;
using DataAccess.DTO.Services;
using DataAccess.DTO.Services.RequestSerivesDTO;
using DataAccess.DTO.Staff;
using DataAccess.DTO.Subscription;
using DataAccess.DTO.Token;
using DataAccess.DTO.User;
using DataAccess.DTO.User.RequestDTO;
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
            CreateMap<GarageDTO, ViewGarageDTO>()
                .ForMember(x => x.GarageBrands, m => m.Ignore());

            CreateMap<InvoicesDTO, Invoices>();
            CreateMap<Invoices, InvoicesDTO>();

            CreateMap<NotificationDTO, Notification>();
            CreateMap<Notification, NotificationDTO>();
            CreateMap<StaffNotification, NotificationDTO>();

            CreateMap<OrdersDTO, Orders>();
            CreateMap<Orders, OrdersDTO>();
            CreateMap<GuestOrderDTO, GuestOrder>();
            CreateMap<GuestOrder, GuestOrderDTO>();
            CreateMap<GuestOrderDTO, OrderDetailDTO>()
                //.ForMember(x => x.BrandID, m => m.MapFrom(a => a.BrandCarID))
                .ForMember(x => x.OrderID, m => m.MapFrom(a => a.GuestOrderID));
            CreateMap<OrdersDTO, OrderDetailDTO>();
            CreateMap<CarDTO, OrderDetailDTO>();
            CreateMap<UsersDTO, OrderDetailDTO>()
                .ForMember(x => x.Email, m => m.MapFrom(a => a.EmailAddress))
                .ForMember(x => x.Status, m => m.Ignore())
                .ForMember(x => x.PhoneNumber, m => m.Ignore());
            CreateMap<UserUpdateDTO, UsersDTO>();

            CreateMap<RefreshTokenDTO, RefreshToken>();
            CreateMap<RefreshToken, RefreshTokenDTO>();
            CreateMap<RoleNameDTO, RoleName>();
            CreateMap<RoleName, RoleNameDTO>();
            
            CreateMap<ServiceDTO, Service>();
            CreateMap<Service, ServiceDTO>();
            CreateMap<AddServiceDTO, ServiceDTO>();

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

            CreateMap<AddStaffDTO, Staff>();
            CreateMap<UpdateStaffDTO, Staff>();
            CreateMap<Staff, StaffDTO>();
            CreateMap<Staff, LoginStaffDTO>();
        }
    }
}
