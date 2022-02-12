using System;

namespace FridgeApp.Application.DTOs
{
    public class ProductInFridgeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public uint DefaultQuantity { get; set; }
        public uint Quantity { get; set; }
        public int Version { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset? UpdatedDateTime { get; set; }
    }
}