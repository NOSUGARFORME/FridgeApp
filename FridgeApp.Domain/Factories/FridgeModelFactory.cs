using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Factories
{
    public class FridgeModelFactory : IFridgeModelFactory
    {
        public FridgeModel Create(FridgeModelId id, FridgeModelName name, FridgeModelYear year)
            => new(id, name, year);
    }
}