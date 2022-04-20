using FridgeApp.Domain.Entities;
using FridgeApp.Infrastructure.Persistence.Config;
using Microsoft.EntityFrameworkCore;

namespace FridgeApp.Infrastructure.Persistence.Contexts;

internal sealed class WriteDbContext : DbContext
{
    public DbSet<Fridge> Fridges { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<FridgeProduct> FridgeProducts { get; set; }
    public DbSet<FridgeModel> FridgeModels { get; set; }

    public WriteDbContext(DbContextOptions<WriteDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var configuration = new WriteConfiguration();
            
        modelBuilder.ApplyConfiguration<Fridge>(configuration);
        modelBuilder.ApplyConfiguration<FridgeProduct>(configuration);
        modelBuilder.ApplyConfiguration<Product>(configuration);
        modelBuilder.ApplyConfiguration<FridgeModel>(configuration);
    }
}