using SuperCar.Shared.Domain.Abstraction;
using System;
using SuperCar.Car.Domain.Event;
using SuperCar.Car.Domain.ValueObjects;

namespace SuperCar.Car.Domain.Aggregate
{
    #region VehicleId
    public record VehicleId : Identity
    {
        public VehicleId(Guid id = default) : base(id == Guid.Empty ? Guid.NewGuid() : id)
        {
        }
    }
    #endregion
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
            Version = vehicleRegistered.Version;
        }
        public void Apply(VehicleDetailsChanged vehicleDetailsChanged)
        {
            Details = VehicleDetails.Create(vehicleDetailsChanged.Fuel,vehicleDetailsChanged.ImageLink,vehicleDetailsChanged.Body,vehicleDetailsChanged.Doors,
                vehicleDetailsChanged.Seats,vehicleDetailsChanged.Trunk);
            Version = vehicleDetailsChanged.Version;
        }
        public void Apply(VehicleRemoved vehicleRemoved)
        {
            Version = vehicleRemoved.Version;
            ChangeState(State.Removed);
        }
        public void ChangeDetails(VehicleId id, VehicleDetails vehicleDetails)
        {
            AddEvent(new VehicleDetailsChanged(id, vehicleDetails, Version));
        }
        public void Remove()
        {
            AddEvent(new VehicleRemoved(Id, Version));
        }
        public bool IsRemoved() => State == State.Removed;
    }
}
