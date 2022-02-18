using System.Threading.Tasks;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Repositories
{
    /// <summary>
    /// Provides methods for manipulating <see cref="FridgeModel"/>.
    /// </summary>
    public interface IFridgeModelRepository
    {
        /// <summary>
        /// Gets <see cref="FridgeModel"/> by identifier.
        /// </summary>
        /// <param name="id">Unique identifier <see cref="FridgeModelId"/>.</param>
        /// <returns><see cref="FridgeModel"/>.</returns>
        Task<FridgeModel> GetAsync(FridgeModelId id);
        
        /// <summary>
        /// Creates <see cref="FridgeModel"/>.
        /// </summary>
        /// <param name="fridgeModel"><see cref="FridgeModel"/> to create.</param>
        Task AddAsync(FridgeModel fridgeModel);
        
        /// <summary>
        /// Updates <see cref="Domain.Entities.FridgeModel"/>.
        /// </summary>
        /// <param name="fridgeModel"><see cref="Domain.Entities.FridgeModel"/> to update.</param>
        Task UpdateAsync(FridgeModel fridgeModel);
        
        /// <summary>
        /// Deletes <see cref="Domain.Entities.FridgeModel"/>.
        /// </summary>
        /// <param name="fridgeModel"><see cref="Domain.Entities.FridgeModel"/> to delete.</param>
        Task DeleteAsync(FridgeModel fridgeModel);
    }
}