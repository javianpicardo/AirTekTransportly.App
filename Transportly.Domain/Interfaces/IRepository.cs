using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Transportly.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll<TKey>(Expression<Func<TEntity, TKey>> orderByClause = null);

        //Other potentially  useful functions that may be needed later
        //TEntity Get(Expression<Func<TEntity, bool>> Selector = null);
        // IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        // TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        //void Add(TEntity entity);
        //void AddRange(IEnumerable<TEntity> entities);
        // void Update(TEntity entity);
        // void Remove(TEntity entity);
        // void RemoveRange(IEnumerable<TEntity> entities);
    }
}
