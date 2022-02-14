using System;
using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Domain.Exceptions
{
    public class ProductNotFoundInFridgeException : FridgeException
    {
        public ProductNotFoundInFridgeException(Guid fridgeId, Guid productId) 
            : base($"Product with id: {productId} was not found in fridge: {fridgeId}.")
        {
        }
    }
}