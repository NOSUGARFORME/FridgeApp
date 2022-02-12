using System;
using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Application.Exceptions
{
    public class FridgeModelNotFoundException : FridgeException
    {
        public FridgeModelNotFoundException(Guid id) 
            : base($"Fridge model with id: '{id}' was not found.")
        {
        }
    }
}