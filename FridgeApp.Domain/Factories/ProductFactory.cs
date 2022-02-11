using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Factories
{
    public sealed class ProductFactory : IProductFactory
    {
        public Product Create(ProductId id, ProductName name, ProductQuantity defaultQuantity)
            => new(id, name, defaultQuantity);
    }
}