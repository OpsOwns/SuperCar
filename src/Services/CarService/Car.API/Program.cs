using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SuperCar.Car.API
{
    public static class Program
    {
        public static async Task Main(string[] args) => await CreateHostBuilder(args).Build().RunAsync();
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging((context, builder) =>
                {
                    builder.Configure(option =>
                    {
                        option.ActivityTrackingOptions = ActivityTrackingOptions.SpanId |
                                                         ActivityTrackingOptions.TraceId |
                                                         ActivityTrackingOptions.ParentId;
                    });
                    var config = context.Configuration.GetSection("Logging");
                    builder.AddConfiguration(config);
                    builder.AddConsole();
                    builder.AddDebug();
                });
    }
}
