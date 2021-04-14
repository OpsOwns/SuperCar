using MediatR;
using SuperCar.CarService.Application.Abstraction;
using SuperCar.CarService.Domain.Aggregate;
using SuperCar.CarService.Domain.Entity;
using SuperCar.CarService.Domain.ValueObjects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.CarService.Application.Functions.Vehicle.Commands
{
    public record UpdateVehicleCommand(Guid AggregateId, string Fuel, string ImageLink, string Body, string Doors, int Seats, bool Trunk) : IRequest<Guid>;
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, Guid>
    {
        private IEventRepository _eventRepository;
        public UpdateVehicleCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Guid> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var result = await _eventRepository.LoadAggregate<VehicleAggregate>(new VehicleId(request.AggregateId),
                 cancellationToken);

            var details = VehicleDetails.Create(request.Fuel, request.ImageLink, request.Body, result.Doors,
                result.Seats, request.Trunk);
            result.ChangeDetails(new VehicleId(request.AggregateId), result.Version, details);
            await _eventRepository.Save(new VehicleId(request.AggregateId), result, cancellationToken);
            return Guid.NewGuid();
        }
    }
}
