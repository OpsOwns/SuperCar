using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperCar.Car.API.Contract
{
    public class ValidationProblemDetails : ProblemDetails
    {
        public ValidationProblemDetails(ValidationException validationException)
        {
            Type = nameof(ValidationException);
            Title = "Validation Error";
            Detail = validationException.Message;
            Status = StatusCodes.Status409Conflict;
        }
        
    }
}
