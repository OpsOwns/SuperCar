using System;
using MassTransit;
using Microsoft.Extensions.Hosting;
using SuperCar.Shared.EventStore.Database.Document;
using SuperCar.Shared.EventStore.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperCar.Shared.EventStore.Database;

namespace EventProcessor
{
    public class ProcessService : IHostedService
    {
        private readonly IBus _bus;
        private readonly IServiceProvider _serviceProvider;
        public ProcessService(IBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _serviceProvider.GetRequiredService<IConfiguration>();
            using var scoped = _serviceProvider.CreateScope();
            var configuration = scoped.ServiceProvider.GetRequiredService<CosmosConfiguration>();
            var client = new CosmosClient(configuration.AccountEndpoint, configuration.AccountKey);
            var context = new CosmosDbContext(client, configuration);
            await context.StartProcessor<EventDocument>("event", Handle);
        }
        private async Task Handle(IReadOnlyCollection<EventDocument> eventDocuments, CancellationToken cancellationToken)
        {
            foreach (var eventDocument in eventDocuments)
            {
                await _bus.Publish(eventDocument, cancellationToken);
            }
        }
        public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
    }
}
