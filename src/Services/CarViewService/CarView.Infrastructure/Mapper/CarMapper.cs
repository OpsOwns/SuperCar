using AutoMapper;
using SuperCar.CarView.Infrastructure.Database.Models;
using SuperCar.CarView.Infrastructure.DTO;

namespace SuperCar.CarView.Infrastructure.Mapper
{
    public class CarMapper : Profile
    {
        public CarMapper()
        {
            CreateMap<CarUpdated, Car>().ForAllMembers(z => z.Ignore());
            CreateMap<CarUpdated, Car>().ForMember((z) => z.Body,
                    x
                        => x.MapFrom(y => y.Body))
                .ForMember((z) => z.Doors,
                    x
                        => x.MapFrom(y => y.Doors))
                .ForMember((z) => z.Fuel,
                    x
                        => x.MapFrom(y => y.Fuel))
                .ForMember((z) => z.Trunk,
                    x
                        => x.MapFrom(y => y.Trunk))
                .ForMember((z) => z.Seats,
                    x
                        => x.MapFrom(y => y.Seats))
                .ForMember((z) => z.ImageLink,
                    x
                        => x.MapFrom(y => y.ImageLink));
            CreateMap<CarDto, Car>().ReverseMap();
        }
    }
}
