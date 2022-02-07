namespace FridgeApp.Domain.ValueObjects
{
    public record FridgeProductQuantity(ushort Value)
    {
        public static implicit operator ushort(FridgeProductQuantity quantity)
            => quantity.Value;

        public static implicit operator FridgeProductQuantity(ushort quantity)
            => new(quantity);
    }
}