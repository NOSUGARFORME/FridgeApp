using System;
using System.Collections.Generic;

namespace FridgeApp.Infrastructure.EF.Models
{
    internal class ProductReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DefaultQuantity { get; set; }
        public int Version { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset? UpdatedDateTime { get; set; }
        
        public ICollection<FridgeProductsReadModel> FridgeProducts { get; set; }
    }
}