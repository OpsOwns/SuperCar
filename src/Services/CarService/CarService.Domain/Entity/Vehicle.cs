using SuperCar.CarService.Domain.Event;
using SuperCar.CarService.Domain.ValueObjects;
using SuperCar.Shared.Domain.Abstraction;

namespace SuperCar.CarService.Domain.Entity
{
    public class Vehicle : AggregateRoot<VehicleId>
    {
        public VehicleDescription Description { get; private set; }
        public VehicleDetails Details { get; private set; }
        protected Vehicle() { }
        public Vehicle(VehicleDescription description, VehicleDetails details) : base(new VehicleId())
        {
            Description = description;
            Details = details;
            AddEvent(new VehicleRegistered(Id, Version, description, details));
        }
        public void Apply(VehicleRegistered vehicleRegistered)
        {
            Details = VehicleDetails.Create(vehicleRegistered.Fuel, vehicleRegistered.ImageLink, vehicleRegistered.Body, vehicleRegistered.Doors,
                vehicleRegistered.Seats, vehicleRegistered.Trunk);
            Description = VehicleDescription.Create(vehicleRegistered.Type, vehicleRegistered.Make, vehicleRegistered.ProductionYear, vehicleRegistered.Color, 
                vehicleRegistered.Engine, vehicleRegistered.Model, vehicleRegistered.Country);
            Id = new VehicleId(vehicleRegistered.AggregateId);
        }
        public void Apply(VehicleDetailsChanged vehicleDetailsChanged)
        {
            Details = VehicleDetails.Create(vehicleDetailsChanged.Fuel,vehicleDetailsChanged.ImageLink,vehicleDetailsChanged.Body,vehicleDetailsChanged.Doors,
                vehicleDetailsChanged.Seats,vehicleDetailsChanged.Trunk);
            Version = vehicleDetailsChanged.Version;
        }
        public void ChangeDetails(VehicleId id, int version, VehicleDetails vehicleDetails)
        {
            ApplyEvent(new VehicleDetailsChanged(id, vehicleDetails, ++version), true);
        }
    }
}
