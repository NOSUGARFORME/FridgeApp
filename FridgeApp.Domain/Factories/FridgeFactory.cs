using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Factories
{
    public class FridgeFactory : IFridgeFactory
    {
        public Fridge Create(FridgeId id, FridgeName name, FridgeOwnerName ownerName, FridgeModel fridgeModel)
            => new(id, name, ownerName, fridgeModel);
        
    }
}