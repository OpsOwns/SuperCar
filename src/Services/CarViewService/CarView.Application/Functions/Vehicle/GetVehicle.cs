using System.Collections.Generic;
using AutoMapper;
using MediatR;
using SuperCar.CarView.Infrastructure.DTO;
using SuperCar.CarView.Infrastructure.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.CarView.Application.Functions.Vehicle
{
    public record GetVehiclesCommand : IRequest<IEnumerable<CarDto>>;
    public class GetVehicleCommandHandler : IRequestHandler<GetVehiclesCommand, IEnumerable<CarDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        public GetVehicleCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CarDto>> Handle(GetVehiclesCommand request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<CarDto>>(await _carRepository.GetCollection(cancellationToken));
        }
    }
}
