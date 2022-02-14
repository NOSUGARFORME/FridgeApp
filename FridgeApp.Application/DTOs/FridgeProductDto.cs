using System;

namespace FridgeApp.Application.DTOs
{
    public class FridgeProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ushort DefaultQuantity { get; set; }
        public ushort Quantity { get; set; }
        public int Version { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset? UpdatedDateTime { get; set; }
    }
}