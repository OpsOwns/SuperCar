using SuperCar.CarService.Domain.Aggregate;
using SuperCar.Shared.Domain.Abstraction;

namespace SuperCar.CarService.Domain.Event
{
    public class VehicleRemoved : DomainEvent
    {
        public VehicleRemoved()
        { }
        public VehicleRemoved(VehicleId aggregateId, int version) : base(aggregateId.Value, version)
        {
        }
    }
}
