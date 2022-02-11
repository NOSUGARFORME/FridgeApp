using FridgeApp.Domain.Exceptions;

namespace FridgeApp.Domain.ValueObjects
{
    public record ProductName
    {
        public string Value { get; }

        public ProductName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyFridgeNameException();
            }

            Value = value;
        }

        public static implicit operator string(ProductName name)
            => name.Value;

        public static implicit operator ProductName(string name)
            => new(name);
    }
}