using System.Threading.Tasks;
using FridgeApp.Domain.Entities;

namespace FridgeApp.Application.Services
{
    /// <summary>
    /// Provides read methods for <see cref="Fridge"/>.
    /// </summary>
    public interface IFridgeReadService
    {
        /// <summary>Determines whether a fridge with the given name exists.</summary>
        /// <returns><c>true</c> if the fridge name was found; otherwise, <c>false</c>.</returns>
        Task<bool> ExistsByNameAsync(string name);
        
    }
}