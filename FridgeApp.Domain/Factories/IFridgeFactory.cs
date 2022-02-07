using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Factories
{
    public interface IFridgeFactory
    {
        Fridge Create(FridgeId id, FridgeName name, FridgeOwnerName ownerName, FridgeModel fridgeModel);
    }
}