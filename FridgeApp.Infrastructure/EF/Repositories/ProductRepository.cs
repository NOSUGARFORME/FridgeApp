using System.Threading.Tasks;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.Repositories;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FridgeApp.Infrastructure.EF.Repositories
{
    /// <inheritdoc />
    internal sealed class ProductRepository : IProductRepository
    {
        private readonly DbSet<Product> _products;
        private readonly WriteDbContext _writeDbContext;

        public ProductRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
            _products = writeDbContext.Products;
        }

        /// <inheritdoc />
        public Task<Product> GetAsync(ProductId id)
            => _products.SingleOrDefaultAsync(f => f.Id == id);

        /// <inheritdoc />
        public async Task AddAsync(Product product)
        {
            await _products.AddAsync(product);
            await _writeDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Product product)
        {
            _products.Update(product);
            await _writeDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Product product)
        {
            _products.Remove(product);
            await _writeDbContext.SaveChangesAsync();
        }
    }
}