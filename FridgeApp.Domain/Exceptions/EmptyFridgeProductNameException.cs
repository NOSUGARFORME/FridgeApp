using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Domain.Exceptions
{
    public class EmptyProductNameException : FridgeException
    {
        public EmptyProductNameException() : base("Product name cannot be empty.")
        {
        }
    }
}