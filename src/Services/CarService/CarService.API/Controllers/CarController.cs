using Microsoft.AspNetCore.Mvc;
using SuperCar.CarService.Application.Functions.Vehicle.Commands;
using SuperCar.Shared.API.Controllers;
using System;
using System.Threading.Tasks;

namespace SuperCar.CarService.API.Controllers
{
    [Route("api/v{version:apiVersion}/cars")]
    [ApiController]
    [ApiVersion("1")]
    public class CarController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterVehicleCommand registerCommand)
        {
            return Ok(await Mediator.Send(registerCommand));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateVehicleCommand updateVehicleCommand)
        {
            return Ok(await Mediator.Send(updateVehicleCommand));
        }
    }
}
