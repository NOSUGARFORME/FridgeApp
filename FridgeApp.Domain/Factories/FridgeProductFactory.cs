using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Factories
{
    public class FridgeProductFactory : IFridgeProductFactory
    {
        public FridgeProduct Create(FridgeProductId id, FridgeProductName name, FridgeProductQuantity quantity,
            FridgeProductQuantity defaultQuantity)
            => new(id, name, quantity, defaultQuantity);
    }
}