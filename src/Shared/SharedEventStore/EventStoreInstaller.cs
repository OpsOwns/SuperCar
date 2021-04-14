using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperCar.Shared.EventStore.Database;

namespace SuperCar.Shared.EventStore
{
    public static class EventStoreInstaller
    {
        public static IServiceCollection AddEventStore(this IServiceCollection service, IConfiguration configuration, string section = "cosmosdb")
        {
            service.AddSingleton(new CosmosConfiguration(configuration, section));
            service.AddScoped<ICosmosDbContext, CosmosDbContext>();
            service.AddSingleton(serviceProvider =>
                new CosmosClient(serviceProvider.GetService<CosmosConfiguration>()?.AccountEndpoint,
                    serviceProvider.GetService<CosmosConfiguration>()?.AccountKey));
            service.AddScoped<IEventStore, EventStore>();
            service.AddHostedService<SeedService>();
            return service;
        }
    }
}
