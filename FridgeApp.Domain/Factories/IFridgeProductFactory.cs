using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Factories
{
    public interface IFridgeProductFactory
    {
        FridgeProduct Create(FridgeProductId id, FridgeProductName name, 
            FridgeProductQuantity quantity, FridgeProductQuantity defaultQuantity);
    }
}