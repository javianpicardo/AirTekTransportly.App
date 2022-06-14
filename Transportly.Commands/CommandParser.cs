using Microsoft.Extensions.DependencyInjection;
using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

namespace Transportly.Commands
{
    public static class CommandParser
    {
        public async static Task<int> Parse(IServiceProvider serviceProvider, string[] args)
        {
            //var commandLineBuilder = new CommandLineBuilder(new RootCommand("Transportly management app"));
            var commandLineBuilder = new CommandLineBuilder(new RootCommand("Welcome to Transportly management app"));
            foreach (Command command in serviceProvider.GetServices<Command>())
            {
                commandLineBuilder.AddCommand(command);
            }

            return await commandLineBuilder
                .UseDefaults()
                .Build()
                .InvokeAsync(args);

        }
    }
}
