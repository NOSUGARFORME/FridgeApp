using System;
using FridgeApp.Shared.Abstractions.Commands;

namespace FridgeApp.Application.Commands
{
    public record CreateProduct(Guid Id, string Name, int DefaultQuantity) : ICommand;
}