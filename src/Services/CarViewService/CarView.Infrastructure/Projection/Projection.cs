using MassTransit;
using SuperCar.CarView.Infrastructure.Interfaces;
using SuperCar.Contracts;
using System.Threading.Tasks;

namespace SuperCar.CarView.Infrastructure.Projection
{
    public class Projection : IConsumer<CarContract>
    {
        private readonly ICarRepository _CarRepository;

        public Projection(ICarRepository carRepository)
        {
            _CarRepository = carRepository;
        }
        //Save To Db
        public async Task Consume(ConsumeContext<CarContract> context)
        {
           
        }
    }
}
