using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System.Threading.Tasks;
using Transportly.Commands;
using Transportly.Infrastructure;

namespace Transportly.App
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var output =  await CommandParser.Parse(BuildHost().Services, args);

            Console.ReadLine();
            return output;
        }

        private static IHost BuildHost()
        {
            return Host.CreateDefaultBuilder()
                .UseSerilog((_, config) =>
                {
                    config.MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                          .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Error)
                          .WriteTo.Console(
                            outputTemplate: "[{Level:u3}] {Message:lj}{NewLine}{Exception}");
                })
                .ConfigureServices(services =>
                {
                    services.AddLogging();
                    services.AddInfrastructure();
                    services.AddCliCommands();
                })
                .Build();
        }
    }
}
