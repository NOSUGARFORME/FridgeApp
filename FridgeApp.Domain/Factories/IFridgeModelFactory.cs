using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Factories
{
    /// <summary>
    /// Provides methods to create <see cref="FridgeModel"/> instances.
    /// </summary>
    public interface IFridgeModelFactory
    {
        /// <summary>
        /// Creates a <see cref="FridgeModel"/> instance.
        /// </summary>
        /// <param name="id">Unique identifier <see cref="FridgeModelId"/>.</param>
        /// <param name="name">Unique name <see cref="FridgeModelName"/>.</param>
        /// <param name="year">Production year <see cref="FridgeModelYear"/>.</param>
        /// <returns>The <see cref="FridgeModel"/> instance.</returns>
        FridgeModel Create(FridgeModelId id, FridgeModelName name, 
            FridgeModelYear year);
    }
}