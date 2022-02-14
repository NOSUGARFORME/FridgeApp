using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgeApp.Application.DTOs;
using FridgeApp.Application.Services;
using FridgeApp.Domain.ValueObjects;
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

        public async Task<IEnumerable<ProductDto>> GetMissingFridgeProducts(FridgeId fridgeId)
            => await _product
                .FromSqlRaw("exec fridges.sp_GetMissingFridgeProducts {0}", Guid.Parse("B11A7F97-B2B5-4116-9AC0-7AD2661A8C82"))
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    DefaultQuantity = p.DefaultQuantity,
                    Name = p.Name
                })
                .ToListAsync();
    }
}