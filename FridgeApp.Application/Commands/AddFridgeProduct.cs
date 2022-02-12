using System;
using FridgeApp.Shared.Abstractions.Commands;

namespace FridgeApp.Application.Commands
{
    public record AddFridgeProduct(Guid FridgeId, Guid ProductId, ushort Quantity) : ICommand;
}