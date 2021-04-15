using SuperCar.Shared.Domain.Interfaces;
using System;

namespace SuperCar.Shared.Domain.Abstraction
{
    public abstract class DomainEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public DateTimeOffset OccurredAt { get; } = DateTime.Now;
        public int Version { get;  set; }
        protected DomainEvent() { }
        protected DomainEvent(Guid aggregateId, int version)
        {
            AggregateId = aggregateId;
            Version = version;
        }
    }
}
