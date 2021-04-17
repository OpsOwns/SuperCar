using Microsoft.EntityFrameworkCore;
using SuperCar.CarView.Infrastructure.Database.Models;
using SuperCar.CarView.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.CarView.Infrastructure.Database
{
    public class CarRepository : ICarRepository
    {
        private readonly CarViewContext _carViewContext;
        public CarRepository(CarViewContext carViewContext) => _carViewContext = carViewContext;
        public async Task Add(Car car, CancellationToken cancellationToken = default)
        {
            await _carViewContext.Cars.AddAsync(car, cancellationToken);
            await _carViewContext.SaveChangesAsync(cancellationToken);
        }
        public async Task Remove(Car car, CancellationToken cancellationToken = default)
        {
            _carViewContext.Cars.Remove(car);
            await _carViewContext.SaveChangesAsync(cancellationToken);
        }
        public async Task Update(Car car, CancellationToken cancellationToken = default)
        {
            _carViewContext.Cars.Update(car);
            await _carViewContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<IEnumerable<Car>> GetCollection(CancellationToken cancellationToken = default) => await _carViewContext.Cars.ToListAsync(cancellationToken);
        public async Task<Car> Get(Guid id, CancellationToken cancellationToken = default) => await _carViewContext.Cars.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
