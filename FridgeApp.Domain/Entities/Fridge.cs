using System.Collections.Generic;
using System.Linq;
using FridgeApp.Domain.Events;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Shared.Abstractions.Domain;

namespace FridgeApp.Domain.Entities
{
    public class Fridge : AggregateRoot<FridgeId>
    {
        private FridgeName _name;
        private OwnerName _ownerName;

        private readonly LinkedList<FridgeProduct> _products = new();

        public FridgeName Name => _name;
        public OwnerName OwnerName => _ownerName;
        public FridgeModel FridgeModel { get; }
        public FridgeModelId FridgeModelId { get; }
        public LinkedList<FridgeProduct> FridgeProducts => _products; 

        private Fridge() { }
        
        internal Fridge(FridgeId id, FridgeName name, OwnerName ownerName, FridgeModel fridgeModel)
        {
            Id = id;
            _name = name;
            _ownerName = ownerName;
            FridgeModel = fridgeModel;
        }
        
        private Fridge(FridgeId id, FridgeName name, OwnerName ownerName, 
            FridgeModel modelName, IEnumerable<Product> products)
            : this(id, name, ownerName, modelName)
        {
            AddProductsWithDefaultQuantity(products);
        }

        public void AddProduct(Product product, ProductQuantity quantity)
        {
            if (_products.Any(p => p.ProductId != product.Id))
            {
                _products.AddLast(new FridgeProduct(this, product, quantity));
                AddEvent(new ProductAdded(this, product, quantity));
                return;
            }

            var existingProduct = _products.SingleOrDefault(p => p.ProductId.Equals(product.Id));
            existingProduct?.AddQuantity(quantity);
      
            AddEvent(new ProductAdded(this, product, quantity));
        }

        public void AddProductsWithDefaultQuantity(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                AddProduct(product, product.DefaultQuantity);
            }
        }
        
        public void RemoveProduct(ProductId productId)
        {
            var fridgeProduct = _products.SingleOrDefault(fp => fp.ProductId == productId);
            
            _products.Remove(fridgeProduct);
            AddEvent(new ProductRemovedEvent(this, productId));
        }
    }
}