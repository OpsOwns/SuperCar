using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using SuperCar.Shared.Domain.Abstraction;
using SuperCar.Shared.EventStore.Database.Document;
using SuperCar.Shared.EventStore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.Shared.EventStore.Database
{
    public class CosmosDbContext : ICosmosDbContext
    {
        private readonly Container _container;
        public CosmosDbContext(CosmosClient cosmos, CosmosConfiguration cosmosConfiguration) =>
            _container = cosmos.GetContainer(cosmosConfiguration.DatabaseId, cosmosConfiguration.ContainerId);

        public async Task Insert(EventDocument eventDocument, PartitionKey partitionKey,
            CancellationToken cancellationToken = default) =>
            await _container.CreateItemAsync(eventDocument, partitionKey, cancellationToken: cancellationToken);

        public async Task<IEnumerable<EventDocument>> Get(Identity identity,CancellationToken cancellationToken = default)
        {
            var query = _container.GetItemLinqQueryable<EventDocument>().Where(z => z.StreamId == identity.Value.ToString());
            var iterator = _container.GetItemQueryIterator<EventDocument>(query.ToQueryDefinition());
            var result = new List<EventDocument>();
            while (iterator.HasMoreResults)
            {
                result.AddRange(await iterator.ReadNextAsync(cancellationToken));
            }
            return result;
        }
    }
}
