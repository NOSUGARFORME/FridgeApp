using System;
using FridgeApp.Domain.Exceptions;

namespace FridgeApp.Domain.ValueObjects
{
    public record FridgeModelYear
    {
        public ushort Value { get; }

        public FridgeModelYear(ushort value)
        {
            if (value < 1970 || value > DateTime.Today.Year)
            {
                throw new InvalidFridgeModelYearException(value);
            }

            Value = value;
        }
        
        
        public static implicit operator ushort(FridgeModelYear year)
            => year.Value;

        public static implicit operator FridgeModelYear(ushort year)
            => new(year);
    }
}