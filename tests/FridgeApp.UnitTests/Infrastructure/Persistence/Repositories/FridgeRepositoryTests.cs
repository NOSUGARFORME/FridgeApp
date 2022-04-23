using System;
using System.Threading.Tasks;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Infrastructure.Persistence.Contexts;
using FridgeApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace FridgeApp.UnitTests.Infrastructure.Persistence.Repositories;

public class FridgeRepositoryTests
{
    private readonly DbContextOptions<WriteDbContext> _dbContextOptions;
    
    public FridgeRepositoryTests()
    {
        var dbName = $"Db_{DateTime.Now.ToFileTimeUtc()}";
        _dbContextOptions = new DbContextOptionsBuilder<WriteDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        
    }

    [Fact]
    public async Task GetAsync_Success_Test()
    {
        var repository = await CreateRepositoryAsync();

        var fridge = await repository.GetAsync(Guid.Parse("03f071a0-d089-49fb-b43f-9def8bb334e0"));
        
        fridge.ShouldNotBeNull();
        fridge.Name.Value.ShouldBe("Fridge_-1");
    }
    
    private async Task<FridgeRepository> CreateRepositoryAsync()
    {
        var context = new WriteDbContext(_dbContextOptions);
        await PopulateDataAsync(context);
        return new FridgeRepository(context);
    }
    
    private async Task PopulateDataAsync(WriteDbContext context)
    {
        var fm = new FridgeModel(Guid.Parse("03f071a0-d089-49fb-b43f-9def8bb334e0"), "FridgeModel_-1", 2020);
        var f = new Fridge(Guid.Parse("03f071a0-d089-49fb-b43f-9def8bb334e0"), "Fridge_-1", new OwnerName("Name", "LastName"), fm);
        await context.Fridges.AddAsync(f);
        
        for (var i = 0; i < 5; i++)
        {
            var fridgeModel = new FridgeModel(Guid.NewGuid(), "FridgeModel_{i}", 2000 + i);
            var fridge = new Fridge(Guid.NewGuid(), $"Fridge_{i}", new OwnerName("Name", i.ToString()), fridgeModel);
            await context.Fridges.AddAsync(fridge);
        }

        await context.SaveChangesAsync();
    }
}
