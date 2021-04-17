using Microsoft.AspNetCore.Mvc;
using SuperCar.Car.Infrastructure.Abstraction;

namespace SuperCar.Car.API.Contract
{
    public class NotFoundProblemDetails : ProblemDetails
    {
        public NotFoundProblemDetails(NotFoundException notFoundException)
        {
            Type = nameof(NotFoundException);
            Title = "Not found Error";
            Detail = notFoundException.Message;
            Status = notFoundException.StatusCode;
        }
    }
}
