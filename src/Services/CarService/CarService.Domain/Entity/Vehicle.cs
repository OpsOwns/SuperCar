using SuperCar.CarService.Domain.ValueObjects;
using SuperCar.Shared.Domain.Abstraction;

namespace SuperCar.CarService.Domain.Entity
{
    public class Vehicle : Entity<VehicleId>
    {
        public VehicleDescription Description { get; private set; }
        public VehicleDetails Details { get; private set; }
        public Vehicle(VehicleDescription description, VehicleDetails details) : base(new VehicleId())
        {
            Description = description;
            Details = details;
        }
    }
}
