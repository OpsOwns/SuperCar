using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SuperCar.Car.API.Contract;
using SuperCar.Car.Application.Installer;
using SuperCar.Car.Infrastructure.Abstraction;
using SuperCar.Shared.EventStore.Installer;
using ValidationProblemDetails = SuperCar.Car.API.Contract.ValidationProblemDetails;

namespace SuperCar.Car.API
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
            services.AddResponseCompression();
            services.AddProblemDetails(cfg =>
            {
                cfg.Map<NotFoundException>(x => new NotFoundProblemDetails(x));
                cfg.Map<ValidationException>(x => new ValidationProblemDetails(x));
            });
            services.AddApplication();
            services.AddEventStore(Configuration, "CosmosDb");
        }

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
            app.UseResponseCompression();
            app.UseProblemDetails();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
