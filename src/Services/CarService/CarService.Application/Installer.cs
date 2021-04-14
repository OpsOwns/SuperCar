﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SuperCar.CarService.Application.Abstraction;
using SuperCar.Shared.API.Controllers.Extensions;
using System.Reflection;

namespace SuperCar.CarService.Application
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
