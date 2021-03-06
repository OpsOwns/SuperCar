using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SuperCar.Shared.Domain.Abstraction;
using SuperCar.Shared.Domain.Interfaces;

namespace SuperCar.Shared.EventStore.Interfaces
{
    public interface IEventStore
    {
        Task Commit(Identity aggregateId,
            int version,
            IReadOnlyCollection<IDomainEvent> events, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<IDomainEvent>> Load(Identity aggregateRootId, CancellationToken cancellationToken = default);
    }
}
