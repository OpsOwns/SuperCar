using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SuperCar.CarView.Application.Installer;
using SuperCar.CarView.Infrastructure.Installer;
using SuperCar.CarView.Infrastructure.Projection;

namespace SuperCar.CarView.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarService", Version = "v1" });
            });
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = false;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new MediaTypeApiVersionReader("v");
            });
            services.AddInfrastructure(Configuration, "ConfigurationDatabase");
            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<Projection>();
                cfg.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host("localhost", rabbitMqHostConfigurator =>
                    {
                        rabbitMqHostConfigurator.Username("guest");
                        rabbitMqHostConfigurator.Password("guest");
                    });
                    configurator.ReceiveEndpoint("car-event", e =>
                    {
                        e.ConfigureConsumer<Projection>(context);
                    });
                });
            });
            services.AddMassTransitHostedService();
            services.AddApplication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger(x =>
                {
                    x.RouteTemplate = "supercar/{documentName}/supercar.json";
                });
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "supercar";
                    c.SwaggerEndpoint("/supercar/v1/supercar.json", "CarService v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
