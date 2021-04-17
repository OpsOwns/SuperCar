using AutoMapper;
using MassTransit;
using Newtonsoft.Json;
using SuperCar.CarView.Infrastructure.Database.Models;
using SuperCar.CarView.Infrastructure.Interfaces;
using SuperCar.Contracts;
using System;
using System.Threading.Tasks;

namespace SuperCar.CarView.Infrastructure.Projection
{
    public class Projection : IConsumer<CarContract>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        public Projection(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<CarContract> context)
        {
            switch (context.Message.CarEvent)
            {
                case CarEvents.VehicleDetailsChanged:
                    var modifyCar = JsonConvert.DeserializeObject<CarUpdated>(context.Message.Payload);
                    await UpdateCar(Guid.Parse(context.Message.StreamId), modifyCar);
                    break;
                case CarEvents.VehicleRegistered:
                    var car = JsonConvert.DeserializeObject<Car>(context.Message.Payload);
                    car.Id = Guid.Parse(context.Message.StreamId);
                    await AddCar(car);
                    break;
                case CarEvents.VehicleRemoved:
                    await RemoveCar(Guid.Parse(context.Message.StreamId));
                    break;
            }
        }
        private async Task UpdateCar(Guid vehicleId, CarUpdated vehicleUpdated)
        {
            var databaseVehicle = await _carRepository.Get(vehicleId);
            if (databaseVehicle is not null)
            {
                await _carRepository.Update(_mapper.Map(vehicleUpdated, databaseVehicle));
            }
        }
        private async Task AddCar(Car car)
        {
            await _carRepository.Add(car);
        }
        private async Task RemoveCar(Guid id)
        {
            var car = await _carRepository.Get(id);
            if (car is not null)
                await _carRepository.Remove(car);
        }
    }
}
