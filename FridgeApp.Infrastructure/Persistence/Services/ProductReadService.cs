using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgeApp.Application.DTOs;
using FridgeApp.Application.Services;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Infrastructure.Persistence.Contexts;
using FridgeApp.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace FridgeApp.Infrastructure.Persistence.Services;

/// <inheritdoc />
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