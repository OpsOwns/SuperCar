using SuperCar.CarService.Domain.Enums;
using SuperCar.Shared.Domain.Abstraction;
using System.Collections.Generic;

namespace SuperCar.CarService.Domain.ValueObjects
{
    public class VehicleDetails : ValueObject
    {
        public Fuel Fuel { get; }
        public string ImageLink { get; }
        public Body Body { get; }
        public int Doors { get; }
        public int Seats { get; }
        public bool Trunk { get; }
        public VehicleDetails(Fuel fuel, string imageLink, Body body, int doors, int seats, bool trunk)
        {
            Fuel = fuel;
            ImageLink = imageLink;
            Body = body;
            Doors = doors;
            Seats = seats;
            Trunk = trunk;
        }
        //Validation
        public static VehicleDetails Create(Fuel fuel, string imageLink, Body body, int doors, int seats, bool trunk)
        {
            return new VehicleDetails(fuel, imageLink, body, doors, seats, trunk);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Fuel;
            yield return ImageLink;
            yield return Body;
            yield return Doors;
            yield return Seats;
            yield return Trunk;
        }
    }
}
