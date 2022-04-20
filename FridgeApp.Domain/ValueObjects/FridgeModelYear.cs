using System;
using FridgeApp.Domain.Exceptions;

namespace FridgeApp.Domain.ValueObjects
{
    public record FridgeModelYear
    {
        public int Value { get; }

        public FridgeModelYear(int value)
        {
            if (value < 1970 || value > DateTime.Today.Year)
            {
                throw new InvalidFridgeModelYearException(value);
            }

            Value = value;
        }
        
        
        public static implicit operator int(FridgeModelYear year)
            => year.Value;

        public static implicit operator FridgeModelYear(int year)
            => new(year);
    }
}