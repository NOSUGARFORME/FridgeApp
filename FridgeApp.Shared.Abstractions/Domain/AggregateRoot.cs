using System.Collections.Generic;
using System.Linq;

namespace FridgeApp.Shared.Abstractions.Domain
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>
    {
        public int Version { get; protected set; }
        public IEnumerable<IDomainEvent> Events => _events;
        
        private readonly List<IDomainEvent> _events = new();

        protected void AddEvent(IDomainEvent @event)
        {
            if (_events.Any() || _versionIncremented) return;
            Version++;
            _versionIncremented = true;
                
            _events.Add(@event);
        }

        public void ClearEvents() => _events.Clear();
        
        private bool _versionIncremented;
        
        protected void IncrementVersion()
        {
            if (_versionIncremented)
            {
                return;
            }

            Version++;
            _versionIncremented = true;
        }
    }
}