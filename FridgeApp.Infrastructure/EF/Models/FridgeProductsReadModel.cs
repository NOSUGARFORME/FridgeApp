using System;

namespace FridgeApp.Infrastructure.EF.Models
{
    internal class FridgeProductsReadModel
    {
        public Guid ProductId { get; set; }
        public ProductReadModel Product { get; set; }
        
        public Guid FridgeId { get; set; }
        public FridgeReadModel Fridge { get; set; }
        
        public uint Quantity { get; set; }
    }
}