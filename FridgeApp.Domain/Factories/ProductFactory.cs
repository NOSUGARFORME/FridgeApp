using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Factories
{
    /// <summary>
    /// A factory for creating <see cref="Product" /> instances.
    /// </summary>
    public sealed class ProductFactory : IProductFactory
    {
        /// <inheritdoc />
        public Product Create(ProductId id, ProductName name, ProductQuantity defaultQuantity)
            => new(id, name, defaultQuantity);
    }
}