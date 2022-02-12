using System.Collections.Generic;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Shared.Abstractions.Domain;

namespace FridgeApp.Domain.Entities
{
    public class FridgeModel : AggregateRoot<FridgeModelId>
    {
        public FridgeModelName FridgeModelName { get; private set; }
        public FridgeModelYear FridgeModelYear { get; private set; }
        public ICollection<Fridge> Fridges { get; private set; }

        internal FridgeModel(FridgeModelId id, FridgeModelName fridgeModelName, FridgeModelYear year)
        {
            Id = id;
            FridgeModelName = fridgeModelName;
            FridgeModelYear = year;
        }
        internal FridgeModel(FridgeModelId id, FridgeModelName fridgeModelName, FridgeModelYear year, ICollection<Fridge> fridges)
            : this(id, fridgeModelName, year)
        {
            Fridges = fridges;
        }
        private FridgeModel() {}
    }
}