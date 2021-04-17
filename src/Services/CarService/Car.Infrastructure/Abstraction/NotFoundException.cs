using Microsoft.AspNetCore.Http;
using SuperCar.Shared.Domain.Abstraction;
using System;

namespace SuperCar.Car.Infrastructure.Abstraction
{
    public class NotFoundException : Exception
    {
        public int StatusCode { get;  }
        public NotFoundException(Identity identity)
            : base($"{identity} has not found")
        {
            StatusCode = StatusCodes.Status404NotFound;
        }
    }
}
