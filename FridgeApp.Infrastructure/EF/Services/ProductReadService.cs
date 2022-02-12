using System.Threading.Tasks;
using FridgeApp.Application.Services;
using FridgeApp.Infrastructure.EF.Contexts;
using FridgeApp.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace FridgeApp.Infrastructure.EF.Services
{
    internal sealed class ProductReadService : IProductReadService
    {
        private readonly DbSet<ProductReadModel> _product;

        public ProductReadService(ReadDbContext context)
            => _product = context.Products;
        
        public Task<bool> ExistsByNameAsync(string name)
            => _product.AnyAsync(f => f.Name == name);
    }
}