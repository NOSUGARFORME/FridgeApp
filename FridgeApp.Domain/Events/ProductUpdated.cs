using FridgeApp.Shared.Abstractions.Domain;

namespace FridgeApp.Domain.Events;

public record ProductUpdated(string Name, int DefaultQuantity) : IDomainEvent;