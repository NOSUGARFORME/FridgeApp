using System.Threading.Tasks;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Repositories
{
    /// <summary>
    /// Provides methods for manipulation <see cref="Product"/>.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Gets <see cref="Product"/> by identifier.
        /// </summary>
        /// <param name="id">Unique identifier <see cref="ProductId"/>.</param>
        /// <returns><see cref="Product"/>.</returns>
        Task<Product> GetAsync(ProductId id);
        
        /// <summary>
        /// Creates <see cref="Product"/>.
        /// </summary>
        /// <param name="product"><see cref="Product"/> to create.</param>
        Task AddAsync(Product product);
        
        /// <summary>
        /// Updates <see cref="Product"/>.
        /// </summary>
        /// <param name="product"><see cref="Product"/> to update.</param>
        Task UpdateAsync(Product product);
        
        /// <summary>
        /// Delete <see cref="Product"/>.
        /// </summary>
        /// <param name="product"><see cref="Product"/> to delete.</param>
        Task DeleteAsync(Product product);
    }
}