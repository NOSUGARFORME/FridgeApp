using FridgeApp.Domain.Entities;
using FridgeApp.Shared.Abstractions.Domain;

namespace FridgeApp.Domain.Events
{
    public record ProductAdded(Fridge Fridge, Product Product, ushort Quantity) : IDomainEvent;
}