using System;
using FridgeApp.Shared.Abstractions.Commands;

namespace FridgeApp.Application.Commands
{
    public record RemoveFridgeProduct(Guid FridgeId, Guid ProductId) : ICommand;
}