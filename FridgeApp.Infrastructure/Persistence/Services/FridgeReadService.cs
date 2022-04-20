using System.Threading.Tasks;
using FridgeApp.Application.Services;
using FridgeApp.Infrastructure.Persistence.Contexts;
using FridgeApp.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace FridgeApp.Infrastructure.Persistence.Services;

/// <inheritdoc />
internal sealed class FridgeReadService : IFridgeReadService
{
    private readonly DbSet<FridgeReadModel> _fridge;

    public FridgeReadService(ReadDbContext context)
        => _fridge = context.Fridges;
        
    /// <inheritdoc />
    public Task<bool> ExistsByNameAsync(string name)
        => _fridge.AnyAsync(f => f.Name == name);
}