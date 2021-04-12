using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SuperCar.CarService.Application;

namespace SuperCar.CarService.API
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
            services.AddProblemDetails();
            services.AddApplication();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger(x =>
                {
                    x.RouteTemplate = "openapi/{documentName}/openapi.json";
                });
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "openapi";
                    c.SwaggerEndpoint("/openapi/v1/openapi.json", "CarService v1");
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
