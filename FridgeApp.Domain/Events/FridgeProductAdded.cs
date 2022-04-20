using FridgeApp.Domain.Entities;
using FridgeApp.Shared.Abstractions.Domain;

namespace FridgeApp.Domain.Events
{
    public record FridgeProductAdded(Fridge Fridge, Product Product, int Quantity) : IDomainEvent;
}