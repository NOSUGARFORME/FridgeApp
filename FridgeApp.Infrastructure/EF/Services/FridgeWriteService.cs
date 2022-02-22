using System.Linq;
using System.Threading.Tasks;
using FridgeApp.Application.Services;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FridgeApp.Infrastructure.EF.Services
{
    /// <inheritdoc />
    internal sealed class FridgeWriteService : IFridgeWriteService
    {
        private readonly DbSet<Fridge> _fridges;
        private readonly DbSet<Product> _products;
        private readonly WriteDbContext _writeDbContext;

        public FridgeWriteService(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
            _products = writeDbContext.Products;
            _fridges = writeDbContext.Fridges;
        }
        
        /// <inheritdoc />
        public async Task RemoveProduct(FridgeId fridgeId, ProductId productId)
        {
            var productToRemove = await _products
                .Include(p => p.FridgeProducts)
                .SingleOrDefaultAsync(p => p.Id == productId);

            var fridgeFromRemove = await _fridges
                .Include(f => f.FridgeProducts)
                .SingleOrDefaultAsync(f => f.Id == fridgeId);

            fridgeFromRemove.FridgeProducts.Remove(
                fridgeFromRemove.FridgeProducts
                    .SingleOrDefault(fp => fp.ProductId == productToRemove.Id)
                );
            await _writeDbContext.SaveChangesAsync();
        }
    }
}