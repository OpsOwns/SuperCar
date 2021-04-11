using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;

namespace SuperCar.Shared.API.Controllers
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected virtual IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
