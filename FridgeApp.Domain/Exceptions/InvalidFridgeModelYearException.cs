using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Domain.Exceptions
{
    public class InvalidFridgeModelYearException : FridgeException
    {
        public InvalidFridgeModelYearException(ushort year) 
            : base($"Value '{year}' is invalid year.")
        {
        }
    }
}