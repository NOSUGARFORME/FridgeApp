using System;
using FridgeApp.Shared.Abstractions.Commands;

namespace FridgeApp.Application.Commands
{
    public record AddDefaultQuantityToMissingFridgeProducts(Guid FridgeId, Guid ProductId, ushort Quantity)
        : ICommand;
}