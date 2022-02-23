using System.Collections.Generic;
using System.Threading.Tasks;
using FridgeApp.Application.DTOs;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Application.Services
{
    /// <summary>
    /// Provides read methods for <see cref="Product"/>.
    /// </summary>
    public interface IProductReadService
    {
        /// <summary>Determines whether a product with the given name exists.</summary>
        /// <returns><c>true</c> if the fridge name was found; otherwise, <c>false</c>.</returns>
        Task<bool> ExistsByNameAsync(string name);
        
        /// <summary>
        /// Gets all missing product from specific fridge.
        /// </summary>
        /// <param name="fridgeId"><see cref="FridgeId"/>.</param>
        Task<IEnumerable<ProductDto>> GetMissingFridgeProducts(FridgeId fridgeId);
    }
}