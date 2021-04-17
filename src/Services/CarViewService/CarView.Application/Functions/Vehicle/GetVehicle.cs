using AutoMapper;
using MediatR;
using SuperCar.CarView.Infrastructure.DTO;
using SuperCar.CarView.Infrastructure.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.CarView.Application.Functions.Vehicle
{
    public record GetVehicleCommand(Guid VehicleId) : IRequest<CarDto>;
    public class GetVehicleCommandHandler : IRequestHandler<GetVehicleCommand, CarDto>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        public GetVehicleCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }
        public async Task<CarDto> Handle(GetVehicleCommand request, CancellationToken cancellationToken)
        {
            return _mapper.Map<CarDto>(await _carRepository.Get(request.VehicleId, cancellationToken));
        }
    }
}
