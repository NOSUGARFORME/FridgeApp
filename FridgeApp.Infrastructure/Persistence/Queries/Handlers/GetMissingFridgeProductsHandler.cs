using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgeApp.Application.DTOs;
using FridgeApp.Application.Queries;
using FridgeApp.Infrastructure.Persistence.Contexts;
using FridgeApp.Infrastructure.Persistence.Models;
using FridgeApp.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace FridgeApp.Infrastructure.Persistence.Queries.Handlers;

internal sealed class GetMissingFridgeProductsHandler : IQueryHandler<GetMissingFridgeProducts, IEnumerable<ProductDto>>
{
    private readonly DbSet<ProductReadModel> _products;

    public GetMissingFridgeProductsHandler(ReadDbContext context)
        => _products = context.Products;

    public async Task<IEnumerable<ProductDto>> HandleAsync(GetMissingFridgeProducts query)
    {
        var products = await _products
            .FromSqlRaw("exec fridges.sp_GetMissingFridgeProducts {0}",
                query.FridgeId)
            .ToListAsync();
            
        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            DefaultQuantity = p.DefaultQuantity
        });
    }
}