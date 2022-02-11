using System.Collections.Generic;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Factories
{
    public sealed class FridgeFactory : IFridgeFactory
    {
        public Fridge Create(FridgeId id, FridgeName name, FridgeOwnerName ownerName, FridgeModel fridgeModel)
            => new(id, name, ownerName, fridgeModel);

        public Fridge CreateWithProducts(FridgeId id, FridgeName name, FridgeOwnerName ownerName,
            FridgeModel fridgeModel, IEnumerable<Product> products)
        {
            var fridge = Create(id, name, ownerName, fridgeModel);
            
            fridge.AddProductsWithDefaultQuantity(products);
            return fridge;
        }
        
    }
}