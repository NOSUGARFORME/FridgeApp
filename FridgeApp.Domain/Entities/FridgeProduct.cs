using FridgeApp.Domain.ValueObjects;
using FridgeApp.Shared.Abstractions.Domain;

namespace FridgeApp.Domain.Entities
{
    public class FridgeProduct : AggregateRoot<FridgeProductId>
    {
        public FridgeProductName Name { get; }
        public FridgeProductQuantity Quantity { get; }
        public FridgeProductQuantity DefaultQuantity { get; }

        internal FridgeProduct(FridgeProductId id, FridgeProductName name, FridgeProductQuantity quantity, FridgeProductQuantity defaultQuantity)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            DefaultQuantity = defaultQuantity;
        }
    }
}