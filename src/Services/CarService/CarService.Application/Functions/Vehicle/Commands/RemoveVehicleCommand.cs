using MediatR;
using SuperCar.CarService.Domain.Aggregate;
using SuperCar.Shared.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.CarService.Application.Functions.Vehicle.Commands
{
    public record RemoveVehicleCommand(Guid VehicleId) : IRequest<bool>;
    public class RemoveVehicleCommandHandler : IRequestHandler<RemoveVehicleCommand, bool>
    {
        private readonly IEventRepository _eventRepository;
        public RemoveVehicleCommandHandler(IEventRepository eventRepository) => _eventRepository = eventRepository;
        public async Task<bool> Handle(RemoveVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicleId = new VehicleId(request.VehicleId);
            var vehicle = await _eventRepository.LoadAggregate<Domain.Aggregate.Vehicle, VehicleId>(vehicleId, cancellationToken);
            if (vehicle.IsRemoved())
                throw new System.ComponentModel.DataAnnotations.ValidationException($"Vehicle with id {vehicle.Id} is already deleted");
            vehicle.Remove();
            await _eventRepository.Save(vehicle.Id, vehicle, cancellationToken);
            return true;
        }
    }
}
