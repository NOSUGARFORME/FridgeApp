using System;
using System.Collections.Generic;
using FridgeApp.Domain.Events;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Shared.Abstractions.Domain;

namespace FridgeApp.Domain.Entities
{
    public class Product : AggregateRoot<ProductId>
    {
        public ProductName Name { get; private set; }
        public ProductQuantity DefaultQuantity { get; private set; }
        
        public IList<FridgeProduct> FridgeProducts { get; private set; }

        internal Product(ProductId id, ProductName name, ProductQuantity defaultQuantity)
        {
            Id = id;
            Name = name;
            DefaultQuantity = defaultQuantity;
        }
        
        internal Product(ProductId id, ProductName name, ProductQuantity defaultQuantity, int version, DateTimeOffset createdDateTime, DateTimeOffset? updatedDateTime)
        {
            Id = id;
            Name = name;
            DefaultQuantity = defaultQuantity;
            Version = version;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }
        
        private Product() {}

        public void Update(string name, int quantity)
        {
            Name = name;
            DefaultQuantity = quantity;
            AddEvent(new ProductUpdated(name, quantity));
        }
    }
}