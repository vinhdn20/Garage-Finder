using Microsoft.Extensions.DependencyInjection;
using Repositories.Interfaces;
using Services.AdminService;
using Services.BrandService;
using Services.CarService;
using Services.CategoryGarageService;
using Services.CategoryService;
using Services.ChatService;
using Services.EmailService;
using Services.FavoriteListService;
using Services.FeedbackService;
using Services.GarageBrandService;
using Services.GarageService;
using Services.ImageGarageService;
using Services.NotificationService;
using Services.OrderService;
using Services.RefreshTokenService;
using Services.ReportService;
using Services.ServiceService;
using Services.StaffService;
using Services.StorageApi;
using Services.SubcriptionService;
using Services.UserService;
using Services.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ConfigService
    {
        public static void ConfigServices(this IServiceCollection services)
        {
            services.AddTransient<IStorageCloud, AzureBlob>();
            services.AddTransient<IUserService, UserService.UserService>();
            services.AddTransient<IOrderService, OrderService.OrderService>();
            services.AddTransient<IEmailService, MailjetService>();
            services.AddTransient<IGarageService, GarageService.GarageService>();
            services.AddTransient<IService, Service>();
            services.AddTransient<IStaffService, StaffService.StaffService>();
            services.AddTransient<IFeedbackService, FeedbackService.FeedbackService>();
            services.AddTransient<IReportService, ReportService.ReportService>();
            services.AddTransient<INotificationService, NotificationService.NotificationService>();
            services.AddTransient<IChatService, ChatService.ChatService>();
            services.AddTransient<ISubcriptionService, SubcriptionService.SubcriptionService>();
            services.AddTransient<IAdminService, AdminService.AdminService>();
            services.AddTransient<IBrandService, BrandService.BrandService>();
            services.AddTransient<ICategoryService, CategoryService.CategoryService>();
            services.AddTransient<IFavoriteListService, FavoriteListService.FavoriteListService>();
            services.AddTransient<ICarService, CarService.CarService>();
            services.AddTransient<IGarageBrandService, GarageBrandService.GarageBrandService>();
            services.AddTransient<ICategoryGarageService, CategoryGarageService.CategoryGarageService>();
            services.AddTransient<IImageGarageService, ImageGarageService.ImageGarageService>();
            services.AddTransient<IRefreshTokenService, RefreshTokenService.RefreshTokenService>();
            services.AddTransient<WebSocketFunction>();
            services.AddTransient<WebsocketSend>();
            services.AddWebSocketService();
        }
    }
}
