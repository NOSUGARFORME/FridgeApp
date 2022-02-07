using FridgeApp.Domain.Exceptions;

namespace FridgeApp.Domain.ValueObjects
{
    public class FridgeModelName
    {
        public string Value { get; }

        public FridgeModelName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyFridgeModelNameException();
            }

            Value = value;
        }

        public static implicit operator string(FridgeModelName name)
            => name.Value;

        public static implicit operator FridgeModelName(string name)
            => new(name);
    }
}