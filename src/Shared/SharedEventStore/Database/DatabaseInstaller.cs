using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.Shared.EventStore.Database
{
    public class DatabaseInstaller : IHostedService
    {
        private readonly IServiceProvider _provider;
        public DatabaseInstaller(IServiceProvider provider)
        {
            _provider = provider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _provider.GetRequiredService<IConfiguration>();
            using var scoped = _provider.CreateScope();
            var configuration = scoped.ServiceProvider.GetRequiredService<CosmosConfiguration>();
            var cosmosClient = new CosmosClient(configuration.AccountEndpoint, configuration.AccountKey);
            var db = await cosmosClient.CreateDatabaseIfNotExistsAsync(configuration.DatabaseId,
                cancellationToken: cancellationToken);
            await db.Database.CreateContainerIfNotExistsAsync(configuration.ContainerId, configuration.PartitionKey,
                cancellationToken: cancellationToken);
            ContainerProperties containerProperties = new(configuration.LeasesId, configuration.PartitionKey);
            await db.Database.CreateContainerIfNotExistsAsync(containerProperties, cancellationToken: cancellationToken);
        }
        public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
    }
}
