using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;
using FridgeApp.Shared.Abstractions.Domain;

namespace FridgeApp.Domain.Events
{
    public record FridgeProductOrderedEvent(Fridge Fridge, FridgeProduct FridgeProduct) : IDomainEvent
    {
    }
}