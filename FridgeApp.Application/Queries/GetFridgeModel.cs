using System;
using FridgeApp.Application.DTOs;
using FridgeApp.Shared.Abstractions.Queries;

namespace FridgeApp.Application.Queries;

public class GetFridgeModel : IQuery<FridgeModelDto>
{
    public Guid Id { get; set; }
}