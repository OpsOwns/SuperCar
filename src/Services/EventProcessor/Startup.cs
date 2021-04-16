using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperCar.Shared.EventStore.Installer;

namespace SuperCar.EventProcessor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit(cfg =>
            {
                cfg.UsingRabbitMq((_, configurator) =>
                {
                    configurator.Host("localhost", rabbitMqHostConfigurator =>
                    {
                        rabbitMqHostConfigurator.Username("guest");
                        rabbitMqHostConfigurator.Password("guest");
                    });
                });
            });
            services.AddHostedService<ProcessService>();
            services.AddMassTransitHostedService();
            services.AddEventStore(Configuration, "CosmosDb");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
        }
    }
}
