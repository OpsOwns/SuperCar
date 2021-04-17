using MediatR;
using SuperCar.Car.Domain.Aggregate;
using SuperCar.Car.Domain.ValueObjects;
using SuperCar.Shared.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.Car.Application.Functions.Vehicle.Commands
{
    public record UpdateVehicleCommand(Guid AggregateId, string Fuel, string ImageLink, string Body, int Doors, int Seats, bool Trunk) : IRequest<Guid>;
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        public UpdateVehicleCommandHandler(IEventRepository eventRepository) => _eventRepository = eventRepository;
        public async Task<Guid> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _eventRepository.LoadAggregate<Domain.Aggregate.Vehicle, VehicleId>(new VehicleId(request.AggregateId),
                 cancellationToken);
            if (vehicle.IsRemoved())
                throw new System.ComponentModel.DataAnnotations.ValidationException(
                    $"Vehicle with id {vehicle.Id} is already deleted");
            var details = VehicleDetails.Create(request.Fuel, request.ImageLink, request.Body, request.Doors, request.Seats, request.Trunk);
            vehicle.ChangeDetails(new VehicleId(request.AggregateId), details);
            await _eventRepository.Save(new VehicleId(request.AggregateId), vehicle, cancellationToken);
            return vehicle.Id.Value;
        }
    }
}
