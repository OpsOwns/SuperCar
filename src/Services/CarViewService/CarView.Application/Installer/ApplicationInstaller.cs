using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SuperCar.CarView.Application.Installer
{
    public static class ApplicationInstaller
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
            return serviceCollection;
        }
    }
}
