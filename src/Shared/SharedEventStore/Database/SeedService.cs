using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SuperCar.Shared.EventStore.Database
{
    public class SeedService : IHostedService
    {
        private readonly IServiceProvider _provider;
        public SeedService(IServiceProvider provider)
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
            await db.Database.CreateContainerIfNotExistsAsync(configuration.ContainerId, "/partitionId",
                cancellationToken: cancellationToken);
        }
        public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
    }
}
