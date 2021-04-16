using System;

namespace SuperCar.CarService.Infrastructure.Abstraction
{
    public class ParameterConstructorException : Exception
    {
        public ParameterConstructorException(Type type)
            : base($"{type.FullName} has no constructor without parameters. This can be either public or private")
        {
        }
    }
}
