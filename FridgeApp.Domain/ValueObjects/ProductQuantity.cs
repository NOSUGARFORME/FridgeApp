namespace FridgeApp.Domain.ValueObjects
{
    public record ProductQuantity(int Value)
    {
        public static implicit operator int(ProductQuantity quantity)
            => quantity.Value;

        public static implicit operator ProductQuantity(int quantity)
            => new(quantity);
    }
}