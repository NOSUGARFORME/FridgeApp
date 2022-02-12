using System;

namespace FridgeApp.Application.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public uint DefaultQuantity { get; set; }
    }
}