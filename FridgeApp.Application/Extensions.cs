using FridgeApp.Domain.Factories;
using FridgeApp.Shared.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace FridgeApp.Application
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method for add Application services.
        /// </summary>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IFridgeFactory, FridgeFactory>();
            services.AddSingleton<IFridgeModelFactory, FridgeModelFactory>();
            services.AddSingleton<IProductFactory, ProductFactory>();
            services.AddCommands();
            
            return services;
        }
    }
}