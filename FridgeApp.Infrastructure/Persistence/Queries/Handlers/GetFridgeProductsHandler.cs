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

internal sealed class GetFridgeProductsHandler : IQueryHandler<GetFridgeProducts, IEnumerable<ProductDto>>
{
    private readonly DbSet<ProductReadModel> _products;

    public GetFridgeProductsHandler(ReadDbContext context)
        => _products = context.Products;
        
    public async Task<IEnumerable<ProductDto>> HandleAsync(GetFridgeProducts query)
    {
        var dbQuery = _products
            .Include(p => p.FridgeProducts)
            .ThenInclude(fp => fp.Product)
            .AsQueryable();

        return await dbQuery
            .SelectMany(p => p.FridgeProducts.Where(fp => fp.FridgeId == query.FridgeId),
                (p, fp) => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    DefaultQuantity = p.DefaultQuantity,
                    Quantity = fp.Quantity
                })
            .AsNoTracking()
            .ToListAsync();
    }
}