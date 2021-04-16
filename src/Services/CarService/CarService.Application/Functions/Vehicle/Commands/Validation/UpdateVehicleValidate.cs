using FluentValidation;
using SuperCar.CarService.Domain.Enums;

namespace SuperCar.CarService.Application.Functions.Vehicle.Commands.Validation
{
    public class UpdateVehicleValidate : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleValidate()
        {
            RuleFor(z => z.Body).IsEnumName(typeof(Body));
        }
    }
}
