using SuperCar.CarService.Domain.Aggregate;
using SuperCar.CarService.Domain.Enums;
using SuperCar.CarService.Domain.ValueObjects;
using SuperCar.Shared.Domain.Abstraction;
using System;

namespace SuperCar.CarService.Domain.Event
{
    public class VehicleRegistered : DomainEvent
    {
        public VehicleType Type { get; }
        public string Make { get; }
        public DateTime ProductionYear { get; }
        public string Color { get; }
        public string Engine { get; }
        public string Model { get; }
        public string Country { get; }
        public Fuel Fuel { get; }
        public string ImageLink { get; }
        public Body Body { get; }
        public int Doors { get; }
        public int Seats { get; }
        public bool Trunk { get; }
        public VehicleRegistered(VehicleId vehicleId , VehicleDescription description, VehicleDetails details) : base(vehicleId.Id)
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
