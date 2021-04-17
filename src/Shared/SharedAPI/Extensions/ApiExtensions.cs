using System;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SuperCar.Shared.API.SeedWork;

namespace SuperCar.Shared.API.Extensions
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddValidation(this IServiceCollection services, Assembly assembly)
        {
            if (assembly is null)
                throw new ArgumentNullException(nameof(assembly));
            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
            return services;
        }

    }
}
