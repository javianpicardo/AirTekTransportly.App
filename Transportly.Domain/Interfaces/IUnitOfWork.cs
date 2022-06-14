using Microsoft.Extensions.Logging;

namespace Transportly.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ILoggerFactory LoggerFactory { get; }
        IOrderRepository OrderRepository { get; }
        IFlightRepository FlightRepository { get; }
        IDayRepository DayRepository { get; }
        IScheduleRepository ScheduleRepository { get; }
    }
}
