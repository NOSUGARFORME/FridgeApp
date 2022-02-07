using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Domain.Exceptions
{
    public class EmptyFridgeNameException : FridgeException
    {
        public EmptyFridgeNameException() : base("Fridge name cannot be empty.")
        {
        }
    }
}