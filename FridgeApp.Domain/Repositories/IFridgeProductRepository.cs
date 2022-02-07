using System.Threading.Tasks;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Repositories
{
    public interface IFridgeProductRepository
    {
        Task<FridgeProduct> GetAsync(FridgeProductId id);
        Task AddAsync(FridgeProduct fridgeProduct);
        Task UpdateAsync(FridgeProduct fridgeProduct);
        Task DeleteAsync(FridgeProduct fridgeProduct);
    }
}