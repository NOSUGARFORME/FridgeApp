using System.Threading.Tasks;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Repositories
{
    public interface IFridgeRepository
    {
        Task<Fridge> GetAsync(FridgeId id);
        Task AddAsync(Fridge fridge);
        Task UpdateAsync(Fridge fridge);
        Task DeleteAsync(Fridge fridge);
    }
}