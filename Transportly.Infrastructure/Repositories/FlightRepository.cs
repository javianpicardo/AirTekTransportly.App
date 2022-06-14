using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Transportly.Domain.Interfaces;
using Transportly.Domain.Models;

namespace Transportly.Infrastructure.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly DataStore dataStore;

        public FlightRepository(DataStore dataStore)
        {
            this.dataStore = dataStore;

        }

        public IEnumerable<Flight> GetAll<TKey>(Expression<Func<Flight, TKey>> orderByClause = null)
        {
            if (orderByClause != null)
            {
                return this.dataStore.Flights.OrderBy(orderByClause);
            }

            return this.dataStore.Flights;
        }
    }
}
