using Microsoft.Extensions.Logging;
using Transportly.Domain.Interfaces;

namespace Transportly.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public ILoggerFactory LoggerFactory { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public IFlightRepository FlightRepository { get; private set; }
        public IDayRepository DayRepository { get; private set; }
        public IScheduleRepository ScheduleRepository { get; private set; }

        public UnitOfWork(
            ILoggerFactory loggerFactory,
            IOrderRepository orderRepo,
            IFlightRepository flightRepo,
            IDayRepository dayRepo,
            IScheduleRepository scheduleRepo
            )
        {
            this.LoggerFactory = loggerFactory;
            this.OrderRepository = orderRepo;
            this.FlightRepository = flightRepo;
            this.DayRepository = dayRepo;
            this.ScheduleRepository = scheduleRepo;
        }
    }
}
