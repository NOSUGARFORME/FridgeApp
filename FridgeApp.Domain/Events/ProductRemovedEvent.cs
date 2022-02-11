using FridgeApp.Domain.Entities;
using FridgeApp.Shared.Abstractions.Domain;

namespace FridgeApp.Domain.Events
{
    public record ProductRemovedEvent(Fridge Fridge, Product Product) : IDomainEvent;
}