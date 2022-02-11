namespace FridgeApp.Domain.ValueObjects
{
    public record ProductQuantity(ushort Value)
    {
        public static implicit operator ushort(ProductQuantity quantity)
            => quantity.Value;

        public static implicit operator ProductQuantity(ushort quantity)
            => new(quantity);
    }
}