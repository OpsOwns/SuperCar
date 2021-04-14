using SuperCar.CarService.Domain.Entity;
using SuperCar.CarService.Domain.Enums;
using SuperCar.CarService.Domain.Event;
using SuperCar.CarService.Domain.ValueObjects;
using SuperCar.Shared.Domain.Abstraction;
using System;

namespace SuperCar.CarService.Domain.Aggregate
{
    public class VehicleAggregate : AggregateRoot
    {
        public VehicleType Type { get; private set; }
        public string Make { get; private set; }
        public DateTime ProductionYear { get; private set; }
        public string Color { get; private set; }
        public string Engine { get; private set; }
        public string Model { get; private set; }
        public string Country { get; private set; }
        public Fuel Fuel { get; private set; }
        public string ImageLink { get; private set; }
        public Body Body { get; private set; }
        public int Doors { get; private set; }
        public int Seats { get; private set; }
        public bool Trunk { get; private set; }
        public VehicleAggregate() { }
        public VehicleAggregate(Vehicle vehicle)
        {
            ApplyEvent(new VehicleRegistered(vehicle.Id, vehicle.Version, vehicle.Description, vehicle.Details),true);
        }
        public void Apply(VehicleRegistered vehicleRegistered)
        {
            Type = vehicleRegistered.Type;
            Make = vehicleRegistered.Make;
            Color = vehicleRegistered.Color;
            Engine = vehicleRegistered.Engine;
            ProductionYear = vehicleRegistered.ProductionYear;
            Model = vehicleRegistered.Model;
            Country = vehicleRegistered.Country;
            Fuel = vehicleRegistered.Fuel;
            ImageLink = vehicleRegistered.ImageLink;
            Body = vehicleRegistered.Body;
            Doors = vehicleRegistered.Doors;
            Seats = vehicleRegistered.Seats;
            Trunk = vehicleRegistered.Trunk;
        }
        public void Apply(VehicleDetailsChanged vehicleDetailsChanged)
        {
            Body = vehicleDetailsChanged.Body;
            Doors = vehicleDetailsChanged.Doors;
            Fuel = vehicleDetailsChanged.Fuel;
            ImageLink = vehicleDetailsChanged.ImageLink;
            Trunk = vehicleDetailsChanged.Trunk;
            Version = vehicleDetailsChanged.Version++;
            Seats = vehicleDetailsChanged.Seats;
        }
        public void ChangeDetails(VehicleId id, int version, VehicleDetails vehicleDetails)
        {
            ApplyEvent(new VehicleDetailsChanged(id,vehicleDetails,version), true);
        }
    }
}
