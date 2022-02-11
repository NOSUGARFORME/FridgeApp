using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Domain.Exceptions
{
    public class ProductNotFoundException : FridgeException
    {
        public ProductNotFoundException(string productName) 
            : base($"Product '{productName}' was not found.")
        {
        }
    }
}