using System.Linq;
using System.Threading.Tasks;
using FridgeApp.Application.DTOs;
using FridgeApp.Application.Queries;
using FridgeApp.Infrastructure.EF.Contexts;
using FridgeApp.Infrastructure.EF.Models;
using FridgeApp.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace FridgeApp.Infrastructure.EF.Queries.Handlers
{
    internal class GetFridgeHandler : IQueryHandler<GetFridge, FridgeDto>
    {
        private readonly DbSet<FridgeReadModel> _fridges;

        public GetFridgeHandler(ReadDbContext context)
            => _fridges = context.Fridges;

        public Task<FridgeDto> HandleAsync(GetFridge query)
            => _fridges
                .Include(f => f.FridgeModel)
                .Include(f => f.Products)
                .ThenInclude(fp => fp.Product)
                .Where(f => f.Id == query.Id)
                .Select(f => f.AsDto())
                .AsNoTracking()
                .SingleOrDefaultAsync();
    }
}