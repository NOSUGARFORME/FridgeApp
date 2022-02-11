using System;
using FridgeApp.Domain.Exceptions;

namespace FridgeApp.Domain.ValueObjects
{
    public record ProductId
    {
        public Guid Value { get; }

        public ProductId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyIdException();
            }
            Value = value;  
        }
        
        public static implicit operator Guid(ProductId id)
            => id.Value;

        public static implicit operator ProductId(Guid id)
            => new(id);
    }
}