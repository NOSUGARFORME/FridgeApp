using System.Threading.Tasks;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Repositories
{
    public interface IFridgeModelRepository
    {
        Task<FridgeModel> GetAsync(FridgeModelId id);
        Task AddAsync(FridgeModel fridgeModel);
        Task UpdateAsync(FridgeModel fridgeModel);
        Task DeleteAsync(FridgeModel fridgeModel);
    }
}