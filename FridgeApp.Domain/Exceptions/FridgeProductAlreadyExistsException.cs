using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Domain.Exceptions
{
    public class ProductAlreadyExistsInFridgeException : FridgeException
    {
        public ProductAlreadyExistsInFridgeException(string fridgeName, string productName) 
            : base($"Fridge: '{fridgeName}' already contains product: '{productName}'")
        {
        }
    }
}