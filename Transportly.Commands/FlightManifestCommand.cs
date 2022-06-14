using Microsoft.Extensions.Logging;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using Transportly.Domain.Interfaces;

namespace Transportly.Commands
{
    public class FlightManifestCommand : Command
    {
        private readonly IUnitOfWork unitOfWork;
        public FlightManifestCommand(IUnitOfWork unitOfWork) : base("manifest", "Used for viewing the order manifest of all flights")
        {
            this.unitOfWork = unitOfWork;
            this.Handler = CommandHandler.Create(Handle);
        }

        private void Handle()
        {
            var logger = this.unitOfWork.LoggerFactory.CreateLogger<FlightManifestCommand>();
            try
            {
                this.unitOfWork.OrderRepository.GetAll(x => x.Identifier).ToList().ForEach(o => logger.LogInformation(o.ToString()));
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
            }
        }
    }
}
