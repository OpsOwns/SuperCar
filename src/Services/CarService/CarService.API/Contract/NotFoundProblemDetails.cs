using Microsoft.AspNetCore.Mvc;
using SuperCar.CarService.Infrastructure.Abstraction;

namespace SuperCar.CarService.API.Contract
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
