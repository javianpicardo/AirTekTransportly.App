using Microsoft.Extensions.DependencyInjection;
using Transportly.Domain.Interfaces;
using Transportly.Infrastructure.Repositories;

namespace Transportly.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<DataStore, DataStore>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IDayRepository, DayRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
