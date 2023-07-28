using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.WebSocket
{
    public static class WebSocketManagerExtensions
    {
        public static IServiceCollection AddWebSocketService(this IServiceCollection services)
        {
            // Besides from adding the WebSocketConnectionManager service,
            services.AddSingleton<WebSocketConnectionManager>();

            // it also searches the executing assembly for types that inherit WebSocketHandler
            // and it registers them as singleton using reflection.
            // so that every request gets the same instance of the message handler, it's important!
            foreach (var type in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var exType in type.ExportedTypes)
                {
                    if (exType.GetTypeInfo().BaseType == typeof(WebSocketHandler))
                    {
                        services.AddSingleton(exType);
                    }
                }
                
            }
            return services;
        }

        // It receives a path and it maps that path using with the WebSocketManagerMiddleware which is passed the specific implementation
        // of WebSocketHandler you provided as argument for the MapWebSocketManager extension method.
        public static IApplicationBuilder MapWebSocketManager(this IApplicationBuilder app, PathString path, WebSocketHandler handler)
        {
            return app.Map(path, (_app) => {
                _app.UseMiddleware<WebSocketManagerMiddleware>(handler);
            });
        }
    }

}
