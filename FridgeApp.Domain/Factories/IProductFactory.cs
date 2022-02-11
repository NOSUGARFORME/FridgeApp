using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Factories
{
    public interface IProductFactory
    {
        Product Create(ProductId id, ProductName name, ProductQuantity defaultQuantity);
    }
}