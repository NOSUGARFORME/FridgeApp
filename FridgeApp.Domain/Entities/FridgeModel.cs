using FridgeApp.Domain.ValueObjects;
using FridgeApp.Shared.Abstractions.Domain;

namespace FridgeApp.Domain.Entities
{
    public class FridgeModel : AggregateRoot<FridgeModelId>
    {
        private FridgeModelName _fridgeModelName;
        private FridgeModelYear _year;
        
        internal FridgeModel(FridgeModelId id, FridgeModelName fridgeModelName, FridgeModelYear year)
        {
            Id = id;
            _fridgeModelName = fridgeModelName;
            _year = year;
        }
    }
}