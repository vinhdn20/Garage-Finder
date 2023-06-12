using Microsoft.Extensions.DependencyInjection;
using Repositories.Implements;
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
            services.AddScoped<IGarageInforRepository, GarageInforRepository>();
        }
    }
}
