using SuperCar.Shared.Domain.Abstraction;
using SuperCar.Shared.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperCar.Shared.EventStore
{
    public class EventStore : IEventStore
    {
        public Task Commit(Identity aggregateId, int version, IReadOnlyCollection<IDomainEvent> events)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<IDomainEvent>> Load(Identity aggregateRootId)
        {
            throw new NotImplementedException();
        }
    }
}
