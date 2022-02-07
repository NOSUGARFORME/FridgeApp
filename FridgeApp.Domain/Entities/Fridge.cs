using System.Collections.Generic;
using System.Linq;
using FridgeApp.Domain.Events;
using FridgeApp.Domain.Exceptions;
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
        
        private Fridge(FridgeId id, FridgeName name, FridgeOwnerName ownerName, FridgeModel modelName, LinkedList<FridgeProduct> products)
            : this(id, name, ownerName, modelName)
        {
            _products = products;
        }

        public void AddProduct(FridgeProduct product)
        {
            var alreadyExists = _products.Any(p => p.Name == product.Name);

            if (alreadyExists)
            {
                throw new FridgeProductAlreadyExistsException(_name, product.Name);
            }

            _products.AddLast(product);
            AddEvent(new FridgeProductAdded(this, product));
        }

        public void AddProducts(IEnumerable<FridgeProduct> products)
        {
            foreach (var product in products)
            {
                AddProduct(product);
            }
        }
        
        public void RemoveFridgeProduct(string productName)
        {
            var product = GetProduct(productName);
            _products.Remove(product);
            AddEvent(new FridgeProductRemovedEvent(this, product));
        }

        private FridgeProduct GetProduct(string productName)
        {
            var product = _products.SingleOrDefault(p => p.Name == productName);

            if (product is null)
            {
                throw new FridgeProductNotFoundException(productName);
            }

            return product;
        }
    }
}