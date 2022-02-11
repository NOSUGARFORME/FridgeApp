using FridgeApp.Domain.ValueObjects;
using FridgeApp.Shared.Abstractions.Domain;

namespace FridgeApp.Domain.Entities
{
    public class Product : AggregateRoot<ProductId>
    {
        public ProductName Name { get; protected set; }
        public ProductQuantity DefaultQuantity { get; protected set; }

        public Product(ProductId id, ProductName name, ProductQuantity defaultQuantity)
        {
            Id = id;
            Name = name;
            DefaultQuantity = defaultQuantity;
        }
    }
}