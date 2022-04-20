using System;
using System.Collections.Generic;

namespace FridgeApp.Infrastructure.Persistence.Models;

internal class FridgeReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public OwnerNameReadModel OwnerName { get; set; }
        
    public int Version { get; set; }
    public DateTimeOffset CreatedDateTime { get; set; }
    public DateTimeOffset? UpdatedDateTime { get; set; }
        
    public Guid FridgeModelId { get; set; }
    public FridgeModelReadModel FridgeModel { get; set; }
        
    public IEnumerable<FridgeProductsReadModel> Products { get; set; }
}