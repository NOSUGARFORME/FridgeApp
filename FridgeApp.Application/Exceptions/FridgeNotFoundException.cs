using System;
using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Application.Exceptions
{
    public class FridgeNotFoundException : BaseNotFoundException
    {
        public FridgeNotFoundException(Guid id) 
            : base($"Fridge with id: '{id}' was not found.")
        {
        }
    }
}