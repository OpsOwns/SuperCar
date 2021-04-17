using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarView.Infrastructure.Installer
{
    public static class InfrastructureInstaller
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service,
            IConfiguration configuration)
        {

            return service;
        }
    }
}
