using Microsoft.AspNetCore.Mvc;
using SuperCar.CarView.Application.Functions.Vehicle;
using SuperCar.CarView.Infrastructure.DTO;
using SuperCar.Shared.API.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperCar.CarView.API.Controllers
{
    [Route("api/v{version:apiVersion}/cars")]
    [ApiController]
    [ApiVersion("1")]
    public class CarViewController : BaseController
    {


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> Get()
        {
            return Ok(await Mediator.Send(new GetVehiclesCommand()));
        }
    }
}
