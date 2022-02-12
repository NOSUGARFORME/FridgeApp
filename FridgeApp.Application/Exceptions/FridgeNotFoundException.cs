using System;
using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Application.Exceptions
{
    public class FridgeNotFoundException : FridgeException
    {
        public FridgeNotFoundException(Guid id) 
            : base($"Fridge with id: '{id}' was not found.")
        {
        }
    }
}