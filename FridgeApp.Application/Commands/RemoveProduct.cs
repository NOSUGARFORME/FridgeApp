using System;
using FridgeApp.Shared.Abstractions.Commands;

namespace FridgeApp.Application.Commands
{
    public record RemoveProduct(Guid Id) : ICommand;
}