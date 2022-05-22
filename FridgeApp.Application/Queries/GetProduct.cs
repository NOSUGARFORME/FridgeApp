using System;
using FridgeApp.Application.DTOs;
using FridgeApp.Shared.Abstractions.Queries;

namespace FridgeApp.Application.Queries;

public class GetProduct : IQuery<ProductDto>
{
    public Guid Id { get; set; }
}