using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Factories
{
    public interface IFridgeModelFactory
    {
        FridgeModel Create(FridgeModelId id, FridgeModelName name, 
            FridgeModelYear year);
    }
}