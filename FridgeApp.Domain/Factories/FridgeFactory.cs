using System.Collections.Generic;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Factories
{
    /// <summary>
    /// A factory for creating <see cref="Fridge" /> instances.
    /// </summary>
    public sealed class FridgeFactory : IFridgeFactory
    {
        /// <inheritdoc />
        public Fridge Create(FridgeId id, FridgeName name, OwnerName ownerName, FridgeModel fridgeModel)
            => new(id, name, ownerName, fridgeModel);

        /// <inheritdoc /> 
        public Fridge CreateWithProducts(FridgeId id, FridgeName name, OwnerName ownerName,
            FridgeModel fridgeModel, IEnumerable<Product> products)
        {
            var fridge = Create(id, name, ownerName, fridgeModel);
            
            fridge.AddProductsWithDefaultQuantity(products);
            return fridge;
        }
        
    }
}