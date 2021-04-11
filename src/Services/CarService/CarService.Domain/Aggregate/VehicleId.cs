using SuperCar.Shared.Domain.Abstraction;
using System;

namespace SuperCar.CarService.Domain.Aggregate
{
    public record VehicleId : Identity
    {
        public VehicleId(Guid id = default) : base(id == Guid.Empty ? Guid.NewGuid() : id)
        {
        }
    }
}
