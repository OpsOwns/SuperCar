using System;

namespace SuperCar.CarService.Infrastructure.Abstraction
{
    public static class AggregateFactory
    {

        public static T CreateAggregate<T>()
        {
            try
            {
                return  (T)Activator.CreateInstance(typeof(T), true);
            }
            catch (MissingMethodException)
            {
                throw new ParameterConstructorException(typeof(T));
            }
        }
    }
}
