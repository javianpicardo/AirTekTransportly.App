using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Transportly.Domain.Interfaces;
using Transportly.Domain.Models;

namespace Transportly.Infrastructure.Repositories
{
    public class DayRepository : IDayRepository
    {
        private readonly DataStore dataStore;
        public DayRepository(DataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public IEnumerable<Day> GetAll<TKey>(Expression<Func<Day, TKey>> orderByClause)
        {
            if (orderByClause != null)
            {
                return this.dataStore.Days.OrderBy(orderByClause);
            }

            return this.dataStore.Days;
        }
    }
}
