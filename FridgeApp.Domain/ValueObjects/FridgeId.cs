using System;
using FridgeApp.Domain.Exceptions;

namespace FridgeApp.Domain.ValueObjects
{
    public record FridgeId
    {
        public Guid Value { get; }

        public FridgeId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyIdException();
            }
            Value = value;  
        }
        
        public static implicit operator Guid(FridgeId id)
            => id.Value;

        public static implicit operator FridgeId(Guid id)
            => new(id);
    }
}