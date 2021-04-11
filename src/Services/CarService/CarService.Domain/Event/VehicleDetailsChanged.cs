using SuperCar.CarService.Domain.Aggregate;
using SuperCar.CarService.Domain.Enums;
using SuperCar.CarService.Domain.ValueObjects;
using SuperCar.Shared.Domain.Abstraction;

namespace SuperCar.CarService.Domain.Event
{
    public class VehicleDetailsChanged : DomainEvent
    {
        public Fuel Fuel { get; }
        public string ImageLink { get; }
        public Body Body { get; }
        public int Doors { get; }
        public int Seats { get; }
        public bool Trunk { get; }
        public VehicleDetailsChanged(VehicleId vehicleId, VehicleDetails details) : base(vehicleId.Id)
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
