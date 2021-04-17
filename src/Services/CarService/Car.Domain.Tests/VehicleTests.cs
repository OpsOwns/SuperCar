using SuperCar.Car.Domain.Aggregate;
using SuperCar.Car.Domain.Enums;
using SuperCar.Car.Domain.ValueObjects;
using System;
using Xunit;

namespace Car.Domain.Tests
{
    public class VehicleTests
    {
        [Fact]
        public void Create_Vehicle_Success()
        {
            var vehicle = new Vehicle(VehicleDescription.Create(VehicleType.Car, "Volvo", new DateTime(1991, 1, 12)
                    , "White", "1.6", "X60", "Germany"),
                VehicleDetails.Create(Fuel.Petrol, null, Body.Hatchback, 5, 5, false));
            Assert.NotNull(vehicle);
        }
    }
}
