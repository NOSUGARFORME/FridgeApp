using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Domain.Exceptions
{
    public class FridgeProductAlreadyExistsException : FridgeException
    {
        public FridgeProductAlreadyExistsException(string fridgeName, string productName) 
            : base($"Fridge: '{fridgeName}' already contains product: '{productName}'")
        {
        }
    }
}