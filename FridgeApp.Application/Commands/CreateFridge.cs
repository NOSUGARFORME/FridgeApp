using System;
using FridgeApp.Shared.Abstractions.Commands;

namespace FridgeApp.Application.Commands
{
    public record CreateFridge(Guid Id, string Name, OwnerWriteModel OwnerName,
        Guid FridgeModelId) : ICommand;

    public record OwnerWriteModel(string FirstName, string LastName);
}