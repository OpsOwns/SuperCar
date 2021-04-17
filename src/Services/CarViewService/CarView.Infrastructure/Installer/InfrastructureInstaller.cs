using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperCar.CarView.Infrastructure.Database;
using SuperCar.CarView.Infrastructure.Database.Configuration;
using SuperCar.CarView.Infrastructure.Interfaces;
using SuperCar.Shared.API.Extensions;

namespace SuperCar.CarView.Infrastructure.Installer
{
    public static class InfrastructureInstaller
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service,
            IConfiguration configuration, string databaseConfigName = "connectionString")
        {
            service.AddSingleton(x => configuration.GetOptions<ConfigurationDatabase>(databaseConfigName));
            service.AddDbContext<CarViewContext>();
            service.AddHostedService<InstallerDatabase>();
            service.AddScoped<ICarRepository, CarRepository>();
            return service;
        }
    }
}
