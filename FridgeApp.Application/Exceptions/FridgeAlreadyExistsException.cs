using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Application.Exceptions
{
    public class FridgeAlreadyExistsException : FridgeException
    {
        public FridgeAlreadyExistsException(string name) 
            : base($"Fridge with name '{name}' already exists.")
        {
        }
    }
}