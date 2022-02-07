using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Domain.Exceptions
{
    public class EmptyFridgeModelNameException : FridgeException
    {
        public EmptyFridgeModelNameException() : base("Fridge model name cannot be empty.")
        {
        }
    }
}