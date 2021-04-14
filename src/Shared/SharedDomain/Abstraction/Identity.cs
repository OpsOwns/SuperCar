using System;

namespace SuperCar.Shared.Domain.Abstraction
{
    public record Identity(Guid Value)
    {
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
