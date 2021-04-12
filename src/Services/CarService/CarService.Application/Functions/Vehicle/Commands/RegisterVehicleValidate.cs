using FluentValidation;
using SuperCar.CarService.Domain.Enums;

namespace SuperCar.CarService.Application.Functions.Vehicle.Commands
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
