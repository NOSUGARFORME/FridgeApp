using FridgeApp.Infrastructure.EF;
using Microsoft.Extensions.DependencyInjection;
using FridgeApp.Shared.Queries;
using Microsoft.Extensions.Configuration;

namespace FridgeApp.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer(configuration);
            services.AddQueries();
            return services;
        }
    }
}