using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgeApp.Application.DTOs;
using FridgeApp.Application.Services;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Infrastructure.EF.Contexts;
using FridgeApp.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace FridgeApp.Infrastructure.EF.Services
{
    /// <summary>
    /// Implement read service for <see cref="Product"/> 
    /// </summary>
    internal sealed class ProductReadService : IProductReadService
    {
        private readonly DbSet<ProductReadModel> _product;

        public ProductReadService(ReadDbContext context)
            => _product = context.Products;
        
        /// <inheritdoc /> 
        public Task<bool> ExistsByNameAsync(string name)
            => _product.AnyAsync(f => f.Name == name);

        /// <inheritdoc />
        public async Task<IEnumerable<ProductDto>> GetMissingFridgeProducts(FridgeId fridgeId)
            => await _product
                .FromSqlRaw("exec fridges.sp_GetMissingFridgeProducts {0}", fridgeId)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    DefaultQuantity = p.DefaultQuantity,
                    Name = p.Name
                })
                .ToListAsync();
    }
}