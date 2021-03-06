using ReflectionMagic;
using SuperCar.Shared.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SuperCar.Shared.Domain.Abstraction
{
    public abstract class AggregateRoot<T> : Entity<T> where T : Identity
    {
        public int Version { get; protected set; }
        protected State State { get; private set; } = State.Created;
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        private readonly List<IDomainEvent> _domainEvents = new();
        protected AggregateRoot() { }
        protected AggregateRoot(T identity) : base(identity)
        {
        }
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
        private void ApplyEvent(IDomainEvent domainEvent, bool isNew)
        {
            this.AsDynamic().Apply(domainEvent);
            if (isNew)
                _domainEvents.Add(domainEvent);
        }
        public void MarkChangesAsCommitted()
        {
            _domainEvents.Clear();
        }
        protected void ChangeState(State state)
        {
            State = state;
        }
    }
}
