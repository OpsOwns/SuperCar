using Microsoft.AspNetCore.Mvc;
using SuperCar.Car.Application.Functions.Vehicle.Commands;
using SuperCar.Shared.API.Controllers;
using System;
using System.Threading.Tasks;
using SuperCar.Car.API.Contract;

namespace SuperCar.Car.API.Controllers
{
    [Route("api/v{version:apiVersion}/cars")]
    [ApiController]
    [ApiVersion("1")]
    public class CarController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterVehicleRequest registerVehicleRequest)
        {
            return Ok(await Mediator.Send(new RegisterVehicleCommand(registerVehicleRequest.VehicleType, 
                registerVehicleRequest.Make, registerVehicleRequest.ProductionYear, registerVehicleRequest.Color, 
                registerVehicleRequest.Engine, registerVehicleRequest.Model, registerVehicleRequest.Country, 
                registerVehicleRequest.Fuel, registerVehicleRequest.ImageLink, registerVehicleRequest.Body, 
                registerVehicleRequest.Doors, registerVehicleRequest.Seats, registerVehicleRequest.Trunk)));
        }
        [HttpPost("remove/{vehicleId}")]
        public async Task<IActionResult> Remove([FromRoute] Guid vehicleId) =>
            Ok(await Mediator.Send(new RemoveVehicleCommand(vehicleId)));

        [HttpPost("update/{vehicleId}")]
        public async Task<IActionResult> Update([FromRoute] Guid vehicleId,
            [FromBody] UpdateDetailsRequest updateDetailsRequest) =>
            Ok(await Mediator.Send(new UpdateVehicleCommand(vehicleId, updateDetailsRequest.Fuel, 
                updateDetailsRequest.ImageLink, updateDetailsRequest.Body, 
                updateDetailsRequest.Doors, updateDetailsRequest.Seats, updateDetailsRequest.Trunk)));
    }
}
