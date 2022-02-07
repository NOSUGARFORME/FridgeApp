using FridgeApp.Domain.Exceptions;

namespace FridgeApp.Domain.ValueObjects
{
    public record FridgeName
    {
        public string Value { get; }

        public FridgeName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyFridgeNameException();
            }

            Value = value;
        }

        public static implicit operator string(FridgeName name)
            => name.Value;

        public static implicit operator FridgeName(string name)
            => new(name);
    }
}