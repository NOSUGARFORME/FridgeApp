using FridgeApp.Application.Services;
using FridgeApp.Domain.Repositories;
using FridgeApp.Infrastructure.EF.Contexts;
using FridgeApp.Infrastructure.EF.Options;
using FridgeApp.Infrastructure.EF.Repositories;
using FridgeApp.Infrastructure.EF.Services;
using FridgeApp.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FridgeApp.Infrastructure.EF
{
    internal static class Extensions
    {
        /// <summary>
        /// Extension method for add database access services.
        /// </summary>
        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFridgeRepository, FridgeRepository>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IFridgeModelRepository, FridgeModelRepository>();
            
            services.AddScoped<IFridgeReadService, FridgeReadService>();
            services.AddScoped<IProductReadService, ProductReadService>();

            services.AddScoped<IFridgeWriteService, FridgeWriteService>();
            
            var option = configuration.GetOptions<DbOptions>("Db");
            services.AddDbContext<ReadDbContext>(ctx =>
                ctx.UseNpgsql(option.ConnectionString));
            services.AddDbContext<WriteDbContext>(ctx =>
                ctx.UseNpgsql(option.ConnectionString));
            
            return services;
        }
    }
}