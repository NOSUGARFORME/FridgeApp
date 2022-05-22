using FridgeApp.Application.Services;
using FridgeApp.Domain.Repositories;
using FridgeApp.Infrastructure.Persistence.Contexts;
using FridgeApp.Infrastructure.Persistence.Options;
using FridgeApp.Infrastructure.Persistence.Repositories;
using FridgeApp.Infrastructure.Persistence.Services;
using FridgeApp.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FridgeApp.Infrastructure.Persistence;

internal static class Extensions
{
    /// <summary>
    /// Extension method for add database access services.
    /// </summary>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<DapperContext>();
        
        services.AddScoped<IFridgeRepository, FridgeRepository>();
        services.AddScoped<IProductRepository,ProductRepository>();
        services.AddScoped<IFridgeModelRepository, FridgeModelRepository>();
            
        services.AddScoped<IFridgeReadService, FridgeReadService>();
        services.AddScoped<IProductReadService, ProductReadService>();

        services.AddScoped<IFridgeWriteService, FridgeWriteService>();
            
        var option = configuration.GetOptions<DbOptions>("ConnectionStrings");
        services.AddDbContext<ReadDbContext>(ctx =>
            ctx.UseNpgsql(option.ConnectionString));
        services.AddDbContext<WriteDbContext>(ctx =>
            ctx.UseNpgsql(option.ConnectionString));
            
        return services;
    }
}