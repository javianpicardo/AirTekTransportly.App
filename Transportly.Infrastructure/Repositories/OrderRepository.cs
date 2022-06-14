using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Transportly.Domain.Interfaces;
using Transportly.Domain.Models;

namespace Transportly.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataStore dataStore;

        public OrderRepository(DataStore dataStore)
        {
            this.dataStore = dataStore;

        }

        public IEnumerable<Order> GetAll<TKey>(Expression<Func<Order, TKey>> orderByClause = null)
        {
            if (orderByClause != null)
            {
                return this.dataStore.Orders.OrderBy(orderByClause);
            }
            return this.dataStore.Orders;
        }
    }
}
