using MediatR;
using SuperCar.Shared.Domain.Abstraction;

namespace SuperCar.Shared.Domain.Interfaces
{
    public interface IDomainEventHandler<T> : INotificationHandler<DomainEventNotification<T>> where T : IDomainEvent
    {
    }
}
