using System.Threading.Tasks;
using FridgeApp.Application.Services;
using FridgeApp.Infrastructure.EF.Contexts;
using FridgeApp.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace FridgeApp.Infrastructure.EF.Services
{
    internal sealed class FridgeReadService : IFridgeReadService
    {
        private readonly DbSet<FridgeReadModel> _fridge;

        public FridgeReadService(ReadDbContext context)
            => _fridge = context.Fridges;
        
        public Task<bool> ExistsByNameAsync(string name)
            => _fridge.AnyAsync(f => f.Name == name);
    }
}