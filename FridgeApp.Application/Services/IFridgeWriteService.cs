using System.Threading.Tasks;
using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Application.Services
{
    public interface IFridgeWriteService
    {
        Task RemoveProduct(FridgeId fridgeId, ProductId productId);
    }
}