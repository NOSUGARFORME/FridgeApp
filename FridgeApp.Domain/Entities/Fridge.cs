using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FridgeApp.Domain.Events;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Shared.Abstractions.Domain;

[assembly: InternalsVisibleTo("FridgeApp.UnitTests")]
namespace FridgeApp.Domain.Entities
{
    public class Fridge : AggregateRoot<FridgeId>
    {
        public FridgeName Name { get; private set; }
        public OwnerName OwnerName { get; private set; }
        public FridgeModel FridgeModel { get; private set; }
        public FridgeModelId FridgeModelId { get; private set; }
        public LinkedList<FridgeProduct> FridgeProducts { get; private set; } = new();

        private Fridge() { }
        
        internal Fridge(FridgeId id, FridgeName name, OwnerName ownerName, FridgeModel fridgeModel)
        {
            Id = id;
            Name = name;
            OwnerName = ownerName;
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
            if (FridgeProducts.All(p => p.Product.Id != product.Id))
            {
                FridgeProducts.AddLast(new FridgeProduct(this, product, quantity));
                AddEvent(new FridgeProductAdded(this, product, quantity));
                return;
            }

            var existingProduct = FridgeProducts.SingleOrDefault(p => p.Product.Id.Equals(product.Id));
            existingProduct?.AddQuantity(quantity);
      
            AddEvent(new FridgeProductAdded(this, product, quantity));
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
            var fridgeProduct = FridgeProducts.SingleOrDefault(fp => fp.Product.Id == productId);
            
            FridgeProducts.Remove(fridgeProduct);
            AddEvent(new FridgeProductRemovedEvent(this, productId));
        }
    }
}