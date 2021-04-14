using SuperCar.Shared.Domain.Abstraction;

namespace SuperCar.Shared.EventStore.Exception
{
    public class ConcurrentException : System.Exception
    {
        public ConcurrentException(Identity identity) : base(
            $"A different version than expected was found on aggregate {identity}")
        {
            
        }
    }
}
