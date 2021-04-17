using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SuperCar.Car.Infrastructure.Abstraction;
using SuperCar.Shared.Domain.Interfaces;
using System.Reflection;
using SuperCar.Shared.API.Extensions;

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
