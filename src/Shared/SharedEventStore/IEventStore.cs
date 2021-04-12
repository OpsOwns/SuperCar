using SuperCar.Shared.Domain.Abstraction;
using SuperCar.Shared.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperCar.Shared.EventStore
{
    public interface IEventStore
    {
        Task Commit(Identity aggregateId,
            int version,
            IReadOnlyCollection<IDomainEvent> events);
        Task<IReadOnlyCollection<IDomainEvent>> Load(Identity aggregateRootId);
    }
}
