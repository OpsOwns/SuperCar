using Microsoft.Azure.Cosmos;
using SuperCar.Shared.Domain.Abstraction;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.Shared.EventStore.Database
{
    public interface ICosmosDbContext
    {
        Task<IEnumerable<EventDocument>> GetDocuments(Identity identity, CancellationToken cancellationToken = default);
        Task Insert(EventDocument eventDocument, PartitionKey partitionKey, CancellationToken cancellationToken = default);
    }
}