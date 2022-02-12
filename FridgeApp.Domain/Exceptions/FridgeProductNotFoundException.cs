using System;
using FridgeApp.Shared.Abstractions.Exceptions;

namespace FridgeApp.Domain.Exceptions
{
    public class ProductNotFoundException : FridgeException
    {
        public ProductNotFoundException(string productName) 
            : base($"Product '{productName}' was not found.")
        {
        }
        
        public ProductNotFoundException(Guid id) 
            : base($"Product with id: '{id}' was not found.")
        {
        }
    }
}