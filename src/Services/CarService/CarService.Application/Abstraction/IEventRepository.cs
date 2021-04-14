using SuperCar.Shared.Domain.Abstraction;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.CarService.Application.Abstraction
{
    public interface IEventRepository
    {
        Task<T> LoadAggregate<T>(Identity id, CancellationToken cancellationToken = default) where T : AggregateRoot;
        Task Save(Identity id, AggregateRoot aggregate, CancellationToken cancellationToken = default);
    }
}