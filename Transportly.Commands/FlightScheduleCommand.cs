using Microsoft.Extensions.Logging;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using Transportly.Domain.Interfaces;

namespace Transportly.Commands
{
    public class FlightScheduleCommand : Command
    {
        private readonly IUnitOfWork unitOfWork;

        public FlightScheduleCommand(IUnitOfWork unitOfWork) : base("schedule", "Used for viewing the planned flight schedule")
        {
            this.unitOfWork = unitOfWork;
            this.Handler = CommandHandler.Create(Handle);
        }

        private void Handle()
        {
            var logger = this.unitOfWork.LoggerFactory.CreateLogger<FlightManifestCommand>();

            try
            {

                this.unitOfWork.FlightRepository.GetAll(x => x.Day).ToList().ForEach(f => logger.LogInformation(f.ToString()));
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
            }

        }
    }
}
