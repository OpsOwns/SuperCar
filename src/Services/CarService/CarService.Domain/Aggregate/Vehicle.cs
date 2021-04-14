using SuperCar.CarService.Domain.Event;
using SuperCar.CarService.Domain.ValueObjects;
using SuperCar.Shared.Domain.Abstraction;

namespace SuperCar.CarService.Domain.Aggregate
{
    public class Vehicle : AggregateRoot<VehicleId>
    {
        public VehicleDescription Description { get; private set; }
        public VehicleDetails Details { get; private set; }
        public Vehicle() : base(new VehicleId()){}
        public Vehicle(VehicleDescription description, VehicleDetails details) : base(new VehicleId())
        {
            Description = description;
            Details = details;
            AddEvent(new VehicleRegistered(Id, Description, details));
        }
        public void Apply(VehicleRegistered vehicleRegistered)
        {
            Description = VehicleDescription.Create(vehicleRegistered.Type, vehicleRegistered.Make, vehicleRegistered.ProductionYear, vehicleRegistered.Color, vehicleRegistered.Engine, vehicleRegistered.Model, vehicleRegistered.Country);
            Details = VehicleDetails.Create(vehicleRegistered.Fuel, vehicleRegistered.ImageLink, vehicleRegistered.Body, vehicleRegistered.Doors, vehicleRegistered.Seats, vehicleRegistered.Trunk);
        }
        public void Apply(VehicleDetailsChanged vehicleDetailsChanged)
        {
            Details = VehicleDetails.Create(vehicleDetailsChanged.Fuel, vehicleDetailsChanged.ImageLink, vehicleDetailsChanged.Body, vehicleDetailsChanged.Doors,
                vehicleDetailsChanged.Seats, vehicleDetailsChanged.Trunk);
        }
        public void ChangeDetails(VehicleDetails details)
        {
            ApplyEvent(new VehicleDetailsChanged(Id, details), true);
        }
    }
}
