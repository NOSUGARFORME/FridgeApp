using System;
using FridgeApp.Domain.Exceptions;

namespace FridgeApp.Domain.ValueObjects
{
    public record Id
    {
        public Guid Value { get; }

        public Id(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyIdException();
            }
            Value = value;  
        }
        
        public static implicit operator Guid(Id id)
            => id.Value;

        public static implicit operator Id(Guid id)
            => new(id);
    }
}