using System.Threading.Tasks;
using Dapper;
using FridgeApp.Application.DTOs;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.Factories;
using FridgeApp.Domain.Repositories;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Infrastructure.Persistence.Contexts;

namespace FridgeApp.Infrastructure.Persistence.Repositories;

/// <inheritdoc />
internal sealed class ProductRepository : IProductRepository
{
    private readonly DapperContext _context;
    private readonly IProductFactory _factory;

    public ProductRepository(DapperContext context, IProductFactory factory)
    {
        _context = context;
        _factory = factory;
    }
    
    /// <inheritdoc />
    public async Task<Product> GetAsync(ProductId id)
    {
        const string sql = "Select * From products Where \"Id\" = @Id";

        await using var connection = _context.CreateConnection();
        
        var result = await connection.QuerySingleOrDefaultAsync<ProductDto>(sql, new { Id = id.Value });
        return _factory.Create(result.Id, result.Name, result.DefaultQuantity, result.Version, result.CreatedDateTime, result.UpdatedDateTime);
    }

    /// <inheritdoc />
    public async Task AddAsync(Product product)
    {
        const string sql = "Insert Into products (\"Id\", \"Name\", \"DefaultQuantity\", \"Version\", \"CreatedDateTime\") " + 
                           "Values (@Id, @Name,@DefaultQuantity, @Version, @CreatedDateTime)";
        
        await using var connection = _context.CreateConnection();
        
        await connection.ExecuteAsync(sql, 
            new {Id = product.Id.Value, Name = product.Name.Value, DefaultQuantity = product.DefaultQuantity.Value, product.Version, product.CreatedDateTime});
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Product product)
    {
        const string sql = "Update products Set \"Name\" = @Name, \"DefaultQuantity\" = @DefaultQuantity, \"Version\" = @Version, \"UpdatedDateTime\" = @UpdatedDateTime Where \"Id\" = @Id";
        
        await using var connection = _context.CreateConnection();

        await connection.ExecuteAsync(sql, new {Id = product.Id.Value, Name = product.Name.Value, DefaultQuantity = product.DefaultQuantity.Value, product.Version, product.UpdatedDateTime});
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Product product)
    {
        const string sql = "Delete From products Where \"Id\" = @Id";
        
        await using var connection = _context.CreateConnection();
        
        await connection.ExecuteAsync(sql, new { Id = product.Id.Value });
    }
}