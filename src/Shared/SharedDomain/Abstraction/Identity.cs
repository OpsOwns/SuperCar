using System;

namespace SuperCar.Shared.Domain.Abstraction
{
    public record Identity(Guid Id)
    {
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
