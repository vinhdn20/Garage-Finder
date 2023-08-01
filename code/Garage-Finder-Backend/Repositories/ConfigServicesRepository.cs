using Microsoft.Extensions.DependencyInjection;
using Repositories.Implements;
using Repositories.Implements.AdminRepository;
using Repositories.Implements.BrandRepository;
using Repositories.Implements.CarRepository;
using Repositories.Implements.CategoryRepository;
using Repositories.Implements.Chat;
using Repositories.Implements.FeedbackRepository;
using Repositories.Implements.Garage;
using Repositories.Implements.NotificationRepository;
using Repositories.Implements.OrderRepository;
using Repositories.Implements.ReportRepository;
using Repositories.Implements.ServiceRepository;
using Repositories.Implements.StaffRepository;
using Repositories.Implements.UserRepository;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public static class ConfigServicesRepository
    {
        public static void ConfigRepository(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IRoleNameRepository, RoleNameRepository>();
            services.AddScoped<IGarageRepository, GarageRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IFavoriteListRepository, FavoriteListRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IGarageBrandRepository, GarageBrandRepository>();
            services.AddScoped<ICategoryGarageRepository, CategoryGarageRepository>();
            services.AddScoped<IImageGarageRepository, ImageGarageRepository>();
            services.AddScoped<IGuestOrderRepository, GuestOrderRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IStaffRefreshTokenRepository, StaffRefreshTokenRepository>();
            services.AddScoped<INotifcationRepository, NotifcationRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
        }
    }
}
