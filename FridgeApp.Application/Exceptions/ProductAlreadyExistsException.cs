using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Application.Exceptions
{
    public class ProductAlreadyExistsException : FridgeException
    {
        public ProductAlreadyExistsException(string name) 
            : base($"Product with name {name} already exists in database.")
        {
        }
    }
}