using FridgeApp.Domain.ValueObjects;

namespace FridgeApp.Domain.Entities
{
    public class FridgeProduct
    {
        public ProductQuantity Quantity { get; private set; }
        public ProductId ProductId { get; private set; }
        public Product Product { get; private set; }
        public Fridge Fridge { get; private set; }
        public FridgeId FridgeId { get; private set; }

        private FridgeProduct() { }
        
        internal FridgeProduct(Fridge fridge, Product product, ProductQuantity quantity)
        {
            Fridge = fridge;
            Product = product;
            Quantity = quantity;
        }

        internal void AddQuantity(ushort quantity)
        {
            Quantity += quantity;
        }
        
        internal void SetQuantity(ushort quantity)
        {
            Quantity = quantity;
        }
    }
}