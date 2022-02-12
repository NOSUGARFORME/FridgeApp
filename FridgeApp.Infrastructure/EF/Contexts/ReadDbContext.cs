using FridgeApp.Infrastructure.EF.Config;
using FridgeApp.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace FridgeApp.Infrastructure.EF.Contexts
{
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
            modelBuilder.HasDefaultSchema("fridges");
            var configuration = new ReadConfiguration();
            
            modelBuilder.ApplyConfiguration<FridgeReadModel>(configuration);
            modelBuilder.ApplyConfiguration<ProductReadModel>(configuration);
            modelBuilder.ApplyConfiguration<FridgeProductsReadModel>(configuration);
            modelBuilder.ApplyConfiguration<FridgeModelReadModel>(configuration);
        }
    }
}