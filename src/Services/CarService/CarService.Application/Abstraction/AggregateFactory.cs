using System;

namespace SuperCar.CarService.Application.Abstraction
{
    public static class AggregateFactory
    {

        public static T CreateAggregate<T>()
        {
            try
            {
                return  (T)Activator.CreateInstance(typeof(T));
            }
            catch (MissingMethodException)
            {
                throw new ParameterConstructorException(typeof(T));
            }
        }
    }
}
