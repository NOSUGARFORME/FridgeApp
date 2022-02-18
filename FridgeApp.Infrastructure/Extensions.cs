using FridgeApp.Infrastructure.EF;
using FridgeApp.Infrastructure.Logging;
using FridgeApp.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;
using FridgeApp.Shared.Queries;
using Microsoft.Extensions.Configuration;

namespace FridgeApp.Infrastructure
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method for add Infrastructure services.
        /// </summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer(configuration);
            services.AddQueries();

            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
                
            return services;
        }
    }
}