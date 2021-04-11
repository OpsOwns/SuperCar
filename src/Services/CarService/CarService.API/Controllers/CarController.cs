using Microsoft.AspNetCore.Mvc;
using SuperCar.Shared.API.Controllers;

namespace SuperCar.CarService.API.Controllers
{
    [Route("api/v{version:apiVersion}/cars")]
    [ApiController]
    [ApiVersion("1")]
    public class CarController : BaseController
    {
    }
}
