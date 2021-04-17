using System.Collections.Generic;
using AutoMapper;
using MediatR;
using SuperCar.CarView.Infrastructure.DTO;
using SuperCar.CarView.Infrastructure.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.CarView.Application.Functions.Vehicle
{
    public record GetVehicleCollectionCommand : IRequest<IEnumerable<CarDto>>;
    public class GetVehicleCollectionCommandHandler : IRequestHandler<GetVehicleCollectionCommand, IEnumerable<CarDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        public GetVehicleCollectionCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CarDto>> Handle(GetVehicleCollectionCommand request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<CarDto>>(await _carRepository.GetCollection(cancellationToken));
        }
    }
}
