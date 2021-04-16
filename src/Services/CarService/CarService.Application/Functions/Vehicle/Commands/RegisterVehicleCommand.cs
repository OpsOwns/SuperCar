using MediatR;
using SuperCar.CarService.Domain.ValueObjects;
using SuperCar.Shared.EventStore.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.CarService.Application.Functions.Vehicle.Commands
{
    public record RegisterVehicleCommand(string VehicleType, string Make, DateTime ProductionYear, string Color,
        string Engine, string Model, string Country, string Fuel, string ImageLink, string Body, int Doors, int Seats,
        bool Trunk) : IRequest<Guid>;
    public class RegisterVehicleCommandHandler : IRequestHandler<RegisterVehicleCommand, Guid>
    {
        private readonly IEventStore _eventStore;
        public RegisterVehicleCommandHandler(IEventStore eventStore) => _eventStore = eventStore;
        public async Task<Guid> Handle(RegisterVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = new Domain.Aggregate.Vehicle(VehicleDescription.Create(request.VehicleType,
                    request.Make, request.ProductionYear, request.Color,
                    request.Engine, request.Model, request.Country),
                VehicleDetails.Create(request.Fuel, request.ImageLink, request.Body, request.Doors, request.Seats,
                    request.Trunk));
            await _eventStore.Commit(vehicle.Id, vehicle.Version, vehicle.DomainEvents, cancellationToken);
             return vehicle.Id.Value;
        }
    }
}
