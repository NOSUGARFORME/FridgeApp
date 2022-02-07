using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Domain.Exceptions
{
    public class EmptyFridgeProductNameException : FridgeException
    {
        public EmptyFridgeProductNameException() : base("Fridge product name cannot be empty.")
        {
        }
    }
}