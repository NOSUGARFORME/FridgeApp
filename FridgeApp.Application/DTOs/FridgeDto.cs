using System;
using System.Collections.Generic;

namespace FridgeApp.Application.DTOs
{
    public class FridgeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public OwnerNameDto OwnerName { get; set; }
        public IEnumerable<ProductInFridgeDto> Products { get; set; }
        public FridgeModelDto FridgeModel { get; set; }
    }
}