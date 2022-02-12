using System;
using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Application.Exceptions
{
    public class ProductNotFoundByIdException : FridgeException
    {
        public ProductNotFoundByIdException(Guid id) 
            : base($"Fridge product with id: '{id}' was not found.")
        {
        }
    }
}