using SuperCar.Shared.Domain.Abstraction;
using System;

namespace SuperCar.Shared.EventStore
{
    public class ConcurrentException : Exception
    {
        public ConcurrentException(Identity identity) : base(
            $"A different version than expected was found on aggregate {identity}")
        {
            
        }
    }
}
