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
        private readonly CosmosClient _cosmosClient;
        private Container _leaseContainer;
        private readonly CosmosConfiguration _cosmosConfiguration;
        private ChangeFeedProcessor _changeFeedProcessor;
        public CosmosDbContext(CosmosClient cosmos, CosmosConfiguration cosmosConfiguration)
        {
            _cosmosClient = cosmos;
            _cosmosConfiguration = cosmosConfiguration;
            _container = cosmos.GetContainer(cosmosConfiguration.DatabaseId, cosmosConfiguration.ContainerId);
        }
        public async Task Insert(EventDocument eventDocument, PartitionKey partitionKey,
            CancellationToken cancellationToken = default) =>
            await _container.CreateItemAsync(eventDocument, partitionKey, cancellationToken: cancellationToken);
        private async Task InitializeLeaseContainer()
        {
            var database = _container.Database;
            ContainerProperties containerProperties = new("supercar-leases", "/id");
            await database.CreateContainerIfNotExistsAsync(containerProperties);
            _leaseContainer = _cosmosClient.GetContainer(_cosmosConfiguration.DatabaseId, "supercar-leases");
        }
        public async Task StartProcessor<T>(string feedName, Container.ChangesHandler<T> handler)
        {
            await InitializeLeaseContainer();
            _changeFeedProcessor = _cosmosClient
                .GetContainer(_cosmosConfiguration.DatabaseId, _cosmosConfiguration.ContainerId)
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
