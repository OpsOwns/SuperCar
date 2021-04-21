using System;
using System.Collections.Generic;
using SuperCar.Car.Domain.Enums;
using SuperCar.Shared.Domain.Abstraction;

namespace SuperCar.Car.Domain.ValueObjects
{
    public class VehicleDetails : ValueObject
    {
        public Fuel Fuel { get; }
        public string ImageLink { get; }
        public Body Body { get; }
        public int Doors { get; }
        public int Seats { get; }
        public bool Trunk { get; }
        private VehicleDetails(Fuel fuel, string imageLink, Body body, int doors, int seats, bool trunk)
        {
            Fuel = fuel;
            ImageLink = imageLink;
            Body = body;
            Doors = doors;
            Seats = seats;
            Trunk = trunk;
        }
        public static VehicleDetails Create(Fuel fuel, string imageLink, Body body, int doors, int seats, bool trunk)
        {
            return new(fuel, imageLink, body, doors, seats, trunk);
        }
        public static VehicleDetails Create(string fuel, string imageLink, string body, int doors, int seats, bool trunk)
        {
            return new(Enum.Parse<Fuel>(fuel), imageLink, Enum.Parse<Body>(body), doors, seats, trunk);
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
