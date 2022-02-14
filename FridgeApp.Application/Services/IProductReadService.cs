using System.Collections.Generic;
using System.Threading.Tasks;
using FridgeApp.Application.DTOs;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Application.Services
{
    public interface IProductReadService
    {
        Task<bool> ExistsByNameAsync(string name);

        Task<IEnumerable<ProductDto>> GetMissingFridgeProducts(FridgeId fridgeId);
    }
}