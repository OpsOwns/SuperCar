using System;
using SuperCar.CarService.Domain.Aggregate;
using SuperCar.CarService.Domain.Enums;
using SuperCar.CarService.Domain.ValueObjects;
using Xunit;

namespace CarService.Domain.Tests
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
