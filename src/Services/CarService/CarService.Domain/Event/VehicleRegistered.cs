using SuperCar.CarService.Domain.Enums;
using SuperCar.CarService.Domain.ValueObjects;
using SuperCar.Shared.Domain.Abstraction;
using System;
using SuperCar.CarService.Domain.Aggregate;

namespace SuperCar.CarService.Domain.Event
{
    public class VehicleRegistered : DomainEvent
    {
        public VehicleType Type { get; init; }
        public string Make { get; init; }
        public DateTime ProductionYear { get; init; }
        public string Color { get; init; }
        public string Engine { get; init; }
        public string Model { get; init; }
        public string Country { get; init; }
        public Fuel Fuel { get; init; }
        public string ImageLink { get; init; }
        public Body Body { get; init; }
        public int Doors { get; init; }
        public int Seats { get; init; }
        public bool Trunk { get; init; }
        public VehicleRegistered() { }
        public VehicleRegistered(VehicleId vehicleId, int version, VehicleDescription description, VehicleDetails details) : base(vehicleId.Value, version)
        {
            Type = description.Type;
            Make = description.Make;
            ProductionYear = description.ProductionYear;
            Color = description.Color;
            Engine = description.Engine;
            Model = description.Model;
            Country = description.Country;
            Fuel = details.Fuel;
            ImageLink = details.ImageLink;
            Body = details.Body;
            Doors = details.Doors;
            Seats = details.Seats;
            Trunk = details.Trunk;
        }
    }
}
