using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Factories
{
    /// <summary>
    /// A factory for creating <see cref="FridgeModel" /> instances.
    /// </summary>
    public sealed class FridgeModelFactory : IFridgeModelFactory
    {
        /// <summary>
        /// Create a <see cref="FridgeModel"/> instance given an <paramref name="id"/>,
        /// <paramref name="name"/> and <paramref name="year"/>.
        /// </summary>
        /// <param name="id">Unique identifier <see cref="FridgeModelId"/>.</param>
        /// <param name="name">Unique name <see cref="FridgeModelName"/>.</param>
        /// <param name="year">Production year <see cref="FridgeModelYear"/>.</param>
        /// <returns>An initialized <see cref="FridgeModel"/> object.</returns>
        public FridgeModel Create(FridgeModelId id, FridgeModelName name, FridgeModelYear year)
            => new(id, name, year);
    }
}