using System;
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
        
        /// <inheritdoc />
        public Product Create(ProductId id, ProductName name, ProductQuantity defaultQuantity, int version, DateTimeOffset createdDateTime, DateTimeOffset? updatedDateTime)
            => new(id, name, defaultQuantity, version, createdDateTime, updatedDateTime);
    }
}