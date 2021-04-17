using FluentValidation;
using SuperCar.Car.Domain.Enums;

namespace SuperCar.Car.Application.Functions.Vehicle.Commands.Validation
{
    public class RegisterVehicleValidate : AbstractValidator<RegisterVehicleCommand>
    {
        public RegisterVehicleValidate()
        {
            RuleFor(z => z.Body).IsEnumName(typeof(Body));
            RuleFor(x => x.Fuel).IsEnumName(typeof(Fuel));
            RuleFor(x => x.VehicleType).IsEnumName(typeof(VehicleType));
        }
    }
}
