using SuperCar.Shared.Domain.Abstraction;
using SuperCar.Shared.Domain.Interfaces;
using SuperCar.Shared.EventStore.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.Car.Infrastructure.Abstraction
{
    public class EventRepository : IEventRepository
    {
        private readonly IEventStore _eventStore;
        public EventRepository(IEventStore eventStore) => _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        public async Task Save<T>(Identity identity, AggregateRoot<T> aggregate, CancellationToken cancellationToken = default) where  T : Identity
        {
            await _eventStore.Commit(identity, aggregate.Version, aggregate.DomainEvents, cancellationToken);
            aggregate.MarkChangesAsCommitted();
        }
        public async Task<T> LoadAggregate<T, TIdenity>(Identity id, CancellationToken cancellationToken = default) where T : AggregateRoot<TIdenity> where TIdenity : Identity
        {
            var aggregate = AggregateFactory.CreateAggregate<T>();
            var events = await _eventStore.Load(id, cancellationToken);
            if (!events.Any())
                throw new NotFoundException(id);
            aggregate.LoadFromHistory(events);
            return aggregate;
        }
    }
}
