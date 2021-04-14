using SuperCar.CarService.Domain.Entity;
using SuperCar.CarService.Domain.Enums;
using SuperCar.CarService.Domain.ValueObjects;
using SuperCar.Shared.Domain.Abstraction;

namespace SuperCar.CarService.Domain.Event
{
    public class VehicleDetailsChanged : DomainEvent
    {
        public Fuel Fuel { get; init; }
        public string ImageLink { get; init; }
        public Body Body { get; init; }
        public int Doors { get; init; }
        public int Seats { get; init; }
        public bool Trunk { get; init; }
        public VehicleDetailsChanged() { }
        public VehicleDetailsChanged(VehicleId vehicleId, VehicleDetails details, int version) : base(vehicleId.Value, version)
        {
            Fuel = details.Fuel;
            ImageLink = details.ImageLink;
            Body = details.Body;
            Doors = details.Doors;
            Seats = details.Seats;
            Trunk = details.Trunk;
        }
    }
}
