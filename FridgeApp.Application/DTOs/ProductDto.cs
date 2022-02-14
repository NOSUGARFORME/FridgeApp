using System;

namespace FridgeApp.Application.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ushort DefaultQuantity { get; set; }
        public ushort Quantity { get; set; }
    }
}