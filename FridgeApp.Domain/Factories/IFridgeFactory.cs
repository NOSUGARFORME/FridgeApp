using System.Collections.Generic;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Factories
{
    /// <summary>
    /// Provides methods to create <see cref="Fridge"/> instances.
    /// </summary>
    public interface IFridgeFactory
    {
        /// <summary>
        /// Creates a <see cref="Fridge"/> instance.
        /// </summary>
        /// <param name="id">Unique identifier <see cref="FridgeId"/>.</param>
        /// <param name="name">Fridge name <see cref="FridgeName"/>.</param>
        /// <param name="ownerName">Fridge <see cref="OwnerName"/>.</param>
        /// <param name="fridgeModel"><see cref="FridgeModel"/>.</param>
        /// <returns>The <see cref="Fridge"/> instance.</returns>
        Fridge Create(FridgeId id, FridgeName name, OwnerName ownerName, FridgeModel fridgeModel);

        /// <summary>
        /// Creates a <see cref="Fridge"/> instance.
        /// </summary>
        /// <param name="id">Unique identifier <see cref="FridgeId"/>.</param>
        /// <param name="name">Fridge name <see cref="FridgeName"/>.</param>
        /// <param name="ownerName">Fridge <see cref="OwnerName"/>.</param>
        /// <param name="fridgeModel"><see cref="FridgeModel"/>.</param>
        /// <param name="products">Fridge initialization <paramref name="products"/>.</param>
        /// <returns>The <see cref="Fridge"/> instance.</returns>
        Fridge CreateWithProducts(FridgeId id, FridgeName name, OwnerName ownerName,
            FridgeModel fridgeModel, IEnumerable<Product> products);
    }
}