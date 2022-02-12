using System.Threading.Tasks;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.Repositories;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FridgeApp.Infrastructure.EF.Repositories
{
    internal sealed class FridgeRepository : IFridgeRepository
    {
        private readonly DbSet<Fridge> _fridges;
        private readonly WriteDbContext _writeDbContext;

        public FridgeRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
            _fridges = writeDbContext.Fridges;
        }

        public Task<Fridge> GetAsync(FridgeId id)
            => _fridges
                .Include(f => f.FridgeProducts)
                .ThenInclude(fp => fp.Product)
                .SingleOrDefaultAsync(f => f.Id == id);

        public async Task AddAsync(Fridge fridge)
        {
            await _fridges.AddAsync(fridge);
            await _writeDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Fridge fridge)
        {
            _fridges.Update(fridge);
            await _writeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Fridge fridge)
        {
            _fridges.Remove(fridge);
            await _writeDbContext.SaveChangesAsync();
        }
    }
}