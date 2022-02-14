using System;
using System.Collections.Generic;
using FridgeApp.Application.DTOs;
using FridgeApp.Shared.Abstractions.Queries;

namespace FridgeApp.Application.Queries
{
    public class GetMissingFridgeProducts : IQuery<IEnumerable<ProductDto>>
    {
        public Guid FridgeId { get; set; }
    }
}