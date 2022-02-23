using System.Threading.Tasks;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Application.Services
{
    /// <summary>
    /// Provides write methods for <see cref="Fridge"/>.
    /// </summary>
    public interface IFridgeWriteService
    {
        /// <summary>
        /// Removes specific product from specific fridge.
        /// </summary>
        /// <param name="fridgeId"><see cref="FridgeId"/>.</param>
        /// <param name="productId"><see cref="ProductId"/>.</param>
        Task RemoveProduct(FridgeId fridgeId, ProductId productId);
    }
}