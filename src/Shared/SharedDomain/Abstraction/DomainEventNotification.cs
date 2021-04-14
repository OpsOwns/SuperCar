using SuperCar.Shared.Domain.Interfaces;
using System;

namespace SuperCar.Shared.Domain.Abstraction
{
    public class DomainEventNotification<TDomainEvent> : IDomainEvent where TDomainEvent : IDomainEvent
    {
        public TDomainEvent DomainEvent { get; }
        public DomainEventNotification(TDomainEvent domainEvent) => DomainEvent = domainEvent;
        public DateTimeOffset OccurredAt { get; } = DateTimeOffset.Now;
    }
}
