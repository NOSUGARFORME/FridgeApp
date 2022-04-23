using System.Linq;
using System.Threading.Tasks;
using FridgeApp.Application.DTOs;
using FridgeApp.Application.Queries;
using FridgeApp.Infrastructure.Persistence.Contexts;
using FridgeApp.Infrastructure.Persistence.Models;
using FridgeApp.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace FridgeApp.Infrastructure.Persistence.Queries.Handlers;

internal sealed class GetFridgeModelHandler : IQueryHandler<GetFridgeModel, FridgeModelDto>
{
    private readonly DbSet<FridgeModelReadModel> _fridgeModels;

    public GetFridgeModelHandler(ReadDbContext context)
    {
        _fridgeModels = context.FridgeModels;
    }

    public Task<FridgeModelDto> HandleAsync(GetFridgeModel query)
        => _fridgeModels
            .Where(fm => fm.Id == query.Id)
            .Select(fm => fm.AsDto())
            .AsNoTracking()
            .SingleOrDefaultAsync();
}
