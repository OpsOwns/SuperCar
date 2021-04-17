using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperCar.CarView.Infrastructure.Database;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCar.CarView.Infrastructure.Installer
{
    public class InstallerDatabase : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public InstallerDatabase(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _serviceProvider.GetRequiredService<IConfiguration>();
            using var scope = _serviceProvider.CreateScope();
            await scope.ServiceProvider.GetRequiredService<CarViewContext>().Database.EnsureCreatedAsync(cancellationToken);
        }
        public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
    }
}
