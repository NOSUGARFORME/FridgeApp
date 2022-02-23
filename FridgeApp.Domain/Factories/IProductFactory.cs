using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Factories
{
    /// <summary>
    /// Provides methods to create <see cref="Product"/> instances.
    /// </summary>
    public interface IProductFactory
    {
        /// <summary>
        /// Creates a <see cref="Product"/> instance.
        /// </summary>
        /// <param name="id">Unique identifier <see cref="ProductId"/>.</param>
        /// <param name="name">Unique <see cref="ProductName"/></param>
        /// <param name="defaultQuantity">Default <see cref="ProductQuantity"/>.</param>
        /// <returns>The <see cref="Product"/> instance.</returns>
        Product Create(ProductId id, ProductName name, ProductQuantity defaultQuantity);
    }
}