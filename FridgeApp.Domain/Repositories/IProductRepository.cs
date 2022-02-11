using System.Threading.Tasks;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(ProductId id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}