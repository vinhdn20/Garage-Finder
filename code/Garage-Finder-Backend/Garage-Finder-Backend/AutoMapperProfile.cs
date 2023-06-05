﻿using AutoMapper;
using DataAccess.DTO;
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
        }
    }
}
