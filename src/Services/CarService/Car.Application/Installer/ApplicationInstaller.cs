using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SuperCar.Car.Infrastructure.Abstraction;
using SuperCar.Shared.API.Controllers.Extensions;
using SuperCar.Shared.Domain.Interfaces;
using System.Reflection;

namespace SuperCar.Car.Application.Installer
{
    public static class ApplicationInstaller
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            var assembly = Assembly.GetExecutingAssembly();
            service.AddMediatR(assembly);
            service.AddAutoMapper(assembly);
            service.AddValidation(assembly);
            service.AddScoped<IEventRepository, EventRepository>();
            return service;
        }
    }
}
