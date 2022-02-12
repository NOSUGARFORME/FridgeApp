using FridgeApp.Domain.Entities;
using FridgeApp.Infrastructure.EF.Config;
using Microsoft.EntityFrameworkCore;

namespace FridgeApp.Infrastructure.EF.Contexts
{
    public class WriteDbContext : DbContext
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
            modelBuilder.HasDefaultSchema("fridges");
            var configuration = new WriteConfiguration();
            
            modelBuilder.ApplyConfiguration<Fridge>(configuration);
            modelBuilder.ApplyConfiguration<FridgeProduct>(configuration);
            modelBuilder.ApplyConfiguration<Product>(configuration);
            modelBuilder.ApplyConfiguration<FridgeModel>(configuration);
        }
    }
}