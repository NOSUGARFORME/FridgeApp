using FridgeApp.Shared.Exceptions;
using FridgeApp.Shared.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FridgeApp.Shared
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method for enabling Shared services.
        /// </summary>
        public static IServiceCollection AddShared(this IServiceCollection services)
        {
            services.AddHostedService<AppInitializer>();
            services.AddScoped<ExceptionMiddleware>();
            return services;
        }

        /// <summary>
        /// Extension method for use Shared middlewares.
        /// </summary>
        public static IApplicationBuilder UseShared(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }
    }
}