using SuperCar.CarView.Infrastructure.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.CarView.Infrastructure.Interfaces
{
    public interface ICarRepository
    {
        Task Add(Car car, CancellationToken cancellationToken = default);
        Task Remove(Car car, CancellationToken cancellationToken = default);
        Task Update(Car car, CancellationToken cancellationToken = default);
        Task<IEnumerable<Car>> GetCollection(CancellationToken cancellationToken = default);
        Task<Car> Get(Guid id, CancellationToken cancellationToken = default);
    }
}
