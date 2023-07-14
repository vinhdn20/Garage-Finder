using Microsoft.Extensions.DependencyInjection;
using Services.ChatService;
using Services.EmailService;
using Services.FeedbackService;
using Services.GarageService;
using Services.NotificationService;
using Services.OrderService;
using Services.ServiceService;
using Services.StaffService;
using Services.StorageApi;
using Services.UserService;
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
            services.AddTransient<INotificationService, NotificationService.NotificationService>();
            services.AddTransient<IChatService, ChatService.ChatService>();
        }
    }
}
