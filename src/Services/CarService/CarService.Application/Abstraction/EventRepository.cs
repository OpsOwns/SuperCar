using SuperCar.Shared.Domain.Abstraction;
using SuperCar.Shared.EventStore.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.CarService.Application.Abstraction
{
    public class EventRepository : IEventRepository
    {
        private readonly IEventStore _eventStore;

        public EventRepository(IEventStore eventStore)
        {
            _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }

        public async Task Save(Identity identity, AggregateRoot aggregate, CancellationToken cancellationToken = default)
        {
            await _eventStore.Commit(identity, aggregate.Version, aggregate.DomainEvents, cancellationToken);
            aggregate.MarkChangesAsCommitted();
        }
        public async Task<T> LoadAggregate<T>(Identity id, CancellationToken cancellationToken = default) where T : AggregateRoot
        {
            var aggregate = AggregateFactory.CreateAggregate<T>();

            var events = await _eventStore.Load(id, cancellationToken);
            if (!events.Any())
                throw new ArgumentException(id.Value.ToString());

            aggregate.LoadFromHistory(events);
            return aggregate;
        }
    }
}
