using FluentValidation;
using SuperCar.Car.Domain.Enums;

namespace SuperCar.Car.Application.Functions.Vehicle.Commands.Validation
{
    public class UpdateVehicleValidate : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleValidate()
        {
            RuleFor(z => z.Body).IsEnumName(typeof(Body));
        }
    }
}
