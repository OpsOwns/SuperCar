using SuperCar.Shared.Domain.Interfaces;
using System;

namespace SuperCar.Shared.Domain.Abstraction
{
    public abstract class DomainEvent : IDomainEvent
    {
        public Guid AggregateId { get; }
        public DateTimeOffset OccurredAt { get; } = DateTime.Now;
        protected DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
        protected DomainEvent(Guid aggregateId, DateTimeOffset occurredAt)
        {
            AggregateId = aggregateId;
            OccurredAt = occurredAt;
        }
    }
}
