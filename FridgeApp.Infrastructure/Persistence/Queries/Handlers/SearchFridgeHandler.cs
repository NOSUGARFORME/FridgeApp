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

internal sealed class SearchFridgeHandler : IQueryHandler<SearchFridge, IEnumerable<FridgeDto>>
{
    private readonly DbSet<FridgeReadModel> _fridges;

    public SearchFridgeHandler(ReadDbContext context)
        => _fridges = context.Fridges;
        
    public async Task<IEnumerable<FridgeDto>> HandleAsync(SearchFridge query)
    {
        var dbQuery = _fridges
            .Include(f => f.FridgeModel)
            .Include(f => f.Products)
            .ThenInclude(fp => fp.Product)
            .AsQueryable();

        if (query.SearchPhrase is not null)
        {
            dbQuery = dbQuery.Where(f => 
                Microsoft.EntityFrameworkCore.EF.Functions.Like(f.Name, $"%{query.SearchPhrase}%"));
        }

        return await dbQuery
            .Select(f => f.AsDto())
            .AsNoTracking()
            .ToListAsync();
    }
}