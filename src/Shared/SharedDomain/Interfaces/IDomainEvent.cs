using MediatR;
using System;

namespace SuperCar.Shared.Domain.Interfaces
{
    public interface IDomainEvent : INotification
    {
        public DateTimeOffset OccurredAt { get; }
    }
}
