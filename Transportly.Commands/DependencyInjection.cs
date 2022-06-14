using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using System.Linq;

namespace Transportly.Commands
{
    public static class DependencyInjection
    {
        public static void AddCliCommands(this IServiceCollection services)
        {
            var commandType = typeof(Command);

            var commands = typeof(DependencyInjection)
                                        .Assembly
                                        .GetExportedTypes()
                                        .Where(x => commandType.IsAssignableFrom(x));

            foreach (var command in commands)
            {
                services.AddSingleton(commandType, command);
            }

        }
    }
}
