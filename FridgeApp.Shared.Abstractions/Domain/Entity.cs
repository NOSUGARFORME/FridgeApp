using System;

namespace FridgeApp.Shared.Abstractions.Domain
{
    /// <summary>
    /// Base entity.
    /// </summary>
    /// <typeparam name="TKey">Type of unique identifier.</typeparam>
    public abstract class Entity<TKey>
    {
        public TKey Id { get; protected set; }
        
        public DateTimeOffset CreatedDateTime { get; protected set; } = DateTimeOffset.Now;
        public DateTimeOffset? UpdatedDateTime { get; protected set; }
    }
}