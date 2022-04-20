using FridgeApp.Infrastructure.Persistence.Config;
using FridgeApp.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace FridgeApp.Infrastructure.Persistence.Contexts;

internal sealed class ReadDbContext : DbContext
{
    public DbSet<FridgeReadModel> Fridges { get; set; }
    public DbSet<ProductReadModel> Products { get; set; }
    public DbSet<FridgeModelReadModel> FridgeModels { get; set; }

    public ReadDbContext(DbContextOptions<ReadDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var configuration = new ReadConfiguration();
            
        modelBuilder.ApplyConfiguration<FridgeReadModel>(configuration);
        modelBuilder.ApplyConfiguration<ProductReadModel>(configuration);
        modelBuilder.ApplyConfiguration<FridgeProductsReadModel>(configuration);
        modelBuilder.ApplyConfiguration<FridgeModelReadModel>(configuration);
    }
}