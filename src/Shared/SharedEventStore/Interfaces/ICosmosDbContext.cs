using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using SuperCar.Shared.Domain.Abstraction;
using SuperCar.Shared.EventStore.Database.Document;

namespace SuperCar.Shared.EventStore.Interfaces
{
    public interface ICosmosDbContext
    {
        Task<IEnumerable<EventDocument>> Get(Identity identity, CancellationToken cancellationToken = default);
        Task Insert(EventDocument eventDocument, PartitionKey partitionKey, CancellationToken cancellationToken = default);
    }
}