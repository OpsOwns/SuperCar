using Newtonsoft.Json;
using SuperCar.CarService.Domain.Aggregate;
using SuperCar.CarService.Domain.Enums;
using SuperCar.CarService.Domain.ValueObjects;
using SuperCar.Shared.Domain.Abstraction;
using System;

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
        [JsonConstructor]
        public VehicleRegistered(string aggregateId) : base(Guid.Parse(aggregateId))
        { }
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
