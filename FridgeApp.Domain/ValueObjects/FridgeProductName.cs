using FridgeApp.Domain.Exceptions;

namespace FridgeApp.Domain.ValueObjects
{
    public record FridgeProductName
    {
        public string Value { get; }

        public FridgeProductName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyFridgeNameException();
            }

            Value = value;
        }

        public static implicit operator string(FridgeProductName name)
            => name.Value;

        public static implicit operator FridgeProductName(string name)
            => new(name);
    }
}