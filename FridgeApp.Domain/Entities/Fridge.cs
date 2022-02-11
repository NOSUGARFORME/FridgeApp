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
        private FridgeOwnerName _ownerName;
        private FridgeModel _modelName;

        private readonly LinkedList<FridgeProduct> _products = new();

        private Fridge() { }
        
        internal Fridge(FridgeId id, FridgeName name, FridgeOwnerName ownerName, FridgeModel fridgeModel)
        {
            Id = id;
            _name = name;
            _ownerName = ownerName;
            _modelName = fridgeModel;
        }
        
        private Fridge(FridgeId id, FridgeName name, FridgeOwnerName ownerName, 
            FridgeModel modelName, IEnumerable<Product> products)
            : this(id, name, ownerName, modelName)
        {
            AddProductsWithDefaultQuantity(products);
        }

        public void AddProduct(Product product, ProductQuantity quantity)
        {
            if (_products.All(p => p.ProductId.Equals(product.Id)))
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
        
        public void RemoveProduct(Product product)
        {
            var fridgeProduct = _products.SingleOrDefault(fp => fp.ProductId == product.Id);
            
            _products.Remove(fridgeProduct);
            AddEvent(new ProductRemovedEvent(this, product));
        }
    }
}