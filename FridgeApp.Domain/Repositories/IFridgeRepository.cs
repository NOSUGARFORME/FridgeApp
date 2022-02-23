using System.Threading.Tasks;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Repositories
{
    /// <summary>
    /// Provides methods for manipulation <see cref="Fridge"/>.
    /// </summary>
    public interface IFridgeRepository
    {
        /// <summary>
        /// Gets <see cref="Fridge"/> by identifier.
        /// </summary>
        /// <param name="id">Unique identifier <see cref="FridgeId"/>.</param>
        /// <returns><see cref="Fridge"/>.</returns>
        Task<Fridge> GetAsync(FridgeId id);
        
        /// <summary>
        /// Creates <see cref="Fridge"/>.
        /// </summary>
        /// <param name="fridge"><see cref="Fridge"/> to create.</param>
        Task AddAsync(Fridge fridge);
        
        /// <summary>
        /// Updates <see cref="Fridge"/>
        /// </summary>
        /// <param name="fridge"><see cref="Fridge"/> to update.</param>
        Task UpdateAsync(Fridge fridge);
        
        /// <summary>
        /// Deletes <see cref="Fridge"/>
        /// </summary>
        /// <param name="fridge"><see cref="Fridge"/> to delete.</param>
        Task DeleteAsync(Fridge fridge);
    }
}