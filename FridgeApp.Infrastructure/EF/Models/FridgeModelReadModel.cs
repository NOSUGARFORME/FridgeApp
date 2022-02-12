using System;
using System.Collections.Generic;

namespace FridgeApp.Infrastructure.EF.Models
{
    internal class FridgeModelReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ushort Year { get; set; }
        
        public ICollection<FridgeReadModel> Fridges { get; set; }
    }
}