using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using SuperCar.Shared.Domain.Abstraction;
using SuperCar.Shared.Domain.Interfaces;
using SuperCar.Shared.EventStore.Database.Document;
using SuperCar.Shared.EventStore.Exception;
using SuperCar.Shared.EventStore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.Shared.EventStore
{
    public class EventStore : IEventStore
    {
        private readonly ICosmosDbContext _cosmosDbContext;
        private readonly JsonSerializerSettings _jsonSerializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore
        };
        public EventStore(ICosmosDbContext cosmosDbContext)
        {
            _cosmosDbContext = cosmosDbContext;
        }
        public async Task Commit(Identity aggregateId, int version, IReadOnlyCollection<IDomainEvent> events, CancellationToken cancellationToken)
        {
            var documentEvents = events.Select(x =>
                new EventDocument(aggregateId, version)
                {
                    AssemblyQualifiedName = x.GetType().AssemblyQualifiedName,
                    Payload = JsonConvert.SerializeObject(x, _jsonSerializerSettings),
                    TimeStamp = x.OccurredAt,
                });

            foreach (var documentEvent in documentEvents)
            {
                try
                {
                    await _cosmosDbContext.Insert(documentEvent, PartitionKey.None, cancellationToken);
                }
                catch (CosmosException ex) when(ex.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new ConcurrentException(aggregateId);
                }
            }
        }
        private static IDomainEvent TransformEvent(string eventSelected, string assemblyName) => 
            JsonConvert.DeserializeObject(eventSelected, Type.GetType(assemblyName)!) as DomainEvent;
        public async Task<IReadOnlyCollection<IDomainEvent>> Load(Identity aggregateRootId, CancellationToken cancellationToken = default)
        {
            var result = (await _cosmosDbContext.Get(aggregateRootId, cancellationToken)).ToList();
            return result.Count is 0 ? new List<IDomainEvent>() : result.Select(eventDocument => TransformEvent(eventDocument.Payload, eventDocument.AssemblyQualifiedName)).ToList();
        }
    }
}
