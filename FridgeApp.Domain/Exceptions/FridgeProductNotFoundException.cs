using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Domain.Exceptions
{
    public class FridgeProductNotFoundException : FridgeException
    {
        public FridgeProductNotFoundException(string productName) 
            : base($"Fridge product '{productName}' was not found.")
        {
        }
    }
}