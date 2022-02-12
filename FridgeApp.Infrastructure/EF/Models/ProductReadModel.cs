using System;
using System.Collections.Generic;

namespace FridgeApp.Infrastructure.EF.Models
{
    internal class ProductReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public uint DefaultQuantity { get; set; }
        
        public ICollection<FridgeProductsReadModel> FridgeProducts { get; set; }
    }
}