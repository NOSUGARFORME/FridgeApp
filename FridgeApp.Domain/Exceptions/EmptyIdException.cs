using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Domain.Exceptions
{
    public class EmptyIdException : FridgeException
    {
        public EmptyIdException() : base("Id cannot be empty.")
        {
        }
    }
}