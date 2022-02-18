using System;
using FridgeApp.Application.Services;
using FridgeApp.Infrastructure.EF;
using FridgeApp.Infrastructure.Logging;
using FridgeApp.Infrastructure.Services;
using FridgeApp.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;
using FridgeApp.Shared.Queries;
using Microsoft.Extensions.Configuration;

namespace FridgeApp.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IProductClientService, ProductClientService>("product", client =>
            {
                client.BaseAddress = new Uri(configuration["ProductApi:BaseUrl"]);
            });

            services.AddSqlServer(configuration);
            services.AddQueries();

            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
                
            return services;
        }
    }
}