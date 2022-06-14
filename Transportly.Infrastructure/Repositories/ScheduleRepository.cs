using Transportly.Domain.Interfaces;
using Transportly.Domain.Models;

namespace Transportly.Infrastructure.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DataStore dataStore;

        public ScheduleRepository(DataStore dataStore)
        {
            this.dataStore = dataStore;

        }

        public Schedule Get()
        {
            return dataStore.Schedule;
        }
    }
}
