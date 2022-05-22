using System;
using FridgeApp.Shared.Abstractions.Commands;

namespace FridgeApp.Application.Commands;

public record UpdateProduct(Guid Id, string Name, int DefaultQuantity) : ICommand;
