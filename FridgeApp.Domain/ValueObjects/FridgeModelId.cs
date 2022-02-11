using System;
using FridgeApp.Domain.Exceptions;

namespace FridgeApp.Domain.ValueObjects
{
    public record FridgeModelId
    {
        public Guid Value { get; }

        public FridgeModelId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyIdException();
            }
            Value = value;  
        }
        
        public static implicit operator Guid(FridgeModelId id)
            => id.Value;

        public static implicit operator FridgeModelId(Guid id)
            => new(id);
    }
}