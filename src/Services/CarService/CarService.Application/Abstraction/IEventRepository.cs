using SuperCar.CarService.Domain.Entity;
using SuperCar.Shared.Domain.Abstraction;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.CarService.Application.Abstraction
{
    public interface IEventRepository
    {
        Task<T> LoadAggregate<T,TY>(Identity id, CancellationToken cancellationToken = default) where T : AggregateRoot<TY> where  TY: Identity;

        Task Save<T>(Identity identity, AggregateRoot<T> aggregate, CancellationToken cancellationToken = default)
            where T : Identity;
    }
}