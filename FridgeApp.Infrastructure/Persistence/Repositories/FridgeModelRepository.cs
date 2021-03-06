using System.Threading.Tasks;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.Repositories;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FridgeApp.Infrastructure.Persistence.Repositories;

/// <inheritdoc />
internal sealed class FridgeModelRepository : IFridgeModelRepository
{
    private readonly DbSet<FridgeModel> _fridgeModels;
    private readonly WriteDbContext _writeDbContext;

    public FridgeModelRepository(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
        _fridgeModels = writeDbContext.FridgeModels;
    }

    /// <inheritdoc />
    public Task<FridgeModel> GetAsync(FridgeModelId id)
        => _fridgeModels.FirstOrDefaultAsync(f => f.Id == id);

    /// <inheritdoc />
    public async Task AddAsync(FridgeModel fridgeModel)
    {
        await _fridgeModels.AddAsync(fridgeModel);
        await _writeDbContext.SaveChangesAsync();
    }
        
    /// <inheritdoc />
    public async Task UpdateAsync(FridgeModel fridgeModel)
    {
        _fridgeModels.Update(fridgeModel);
        await _writeDbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(FridgeModel fridgeModel)
    {
        _fridgeModels.Remove(fridgeModel);
        await _writeDbContext.SaveChangesAsync();
    }
}