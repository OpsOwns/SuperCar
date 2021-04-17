using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using SuperCar.Shared.Domain.Abstraction;
using SuperCar.Shared.EventStore.Database.Document;
using SuperCar.Shared.EventStore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.Shared.EventStore.Database
{
    public class CosmosDbContext : ICosmosDbContext
    {
        private readonly Container _container;
        private readonly Container _leaseContainer;
        private ChangeFeedProcessor _changeFeedProcessor;
        public CosmosDbContext(CosmosClient cosmos, CosmosConfiguration cosmosConfiguration)
        {
            _container = cosmos.GetContainer(cosmosConfiguration.DatabaseId, cosmosConfiguration.ContainerId);
            _leaseContainer = cosmos.GetContainer(cosmosConfiguration.DatabaseId, cosmosConfiguration.LeasesId);
        }
        public async Task Insert(EventDocument eventDocument, PartitionKey partitionKey,
            CancellationToken cancellationToken = default) =>
            await _container.CreateItemAsync(eventDocument, partitionKey, cancellationToken: cancellationToken);
        public async Task StartProcessor<T>(string feedName, Container.ChangesHandler<T> handler)
        {
            _changeFeedProcessor = _container
                .GetChangeFeedProcessorBuilder(feedName, handler).WithLeaseContainer(_leaseContainer)
                .WithInstanceName(feedName).WithPollInterval(TimeSpan.FromMilliseconds(50)).Build();
            await _changeFeedProcessor.StartAsync();
        }
        public async Task StopProcessor() => await _changeFeedProcessor.StopAsync();
        public async Task<IEnumerable<EventDocument>> Get(Identity identity, CancellationToken cancellationToken = default)
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
