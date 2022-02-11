using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Domain.Exceptions
{
    public class ProductAlreadyExistsException : FridgeException
    {
        public ProductAlreadyExistsException(string fridgeName, string productName) 
            : base($"Fridge: '{fridgeName}' already contains product: '{productName}'")
        {
        }
    }
}