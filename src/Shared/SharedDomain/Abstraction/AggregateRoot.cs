using ReflectionMagic;
using SuperCar.Shared.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SuperCar.Shared.Domain.Abstraction
{
    public abstract class AggregateRoot
    {
        public int Version { get; protected set; }
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        private readonly List<IDomainEvent> _domainEvents = new();
        protected AggregateRoot() { Version = 0; }
        public void LoadFromHistory(IEnumerable<IDomainEvent> events)
        {
            var domainEvents = events as IDomainEvent[] ?? events.ToArray();
            foreach (var @event in domainEvents)
            {
                ApplyEvent(@event, false);
            }
        }
        protected void AddEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        protected void ApplyEvent(IDomainEvent domainEvent, bool isNew)
        {
            this.AsDynamic().Apply(domainEvent);
            if (isNew)
                _domainEvents.Add(domainEvent);
        }
        public void MarkChangesAsCommitted()
        {
            _domainEvents.Clear();
        }
    }
}
