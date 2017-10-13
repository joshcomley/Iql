using System;
using System.Collections.Generic;

namespace Iql.Queryable.Data
{
    public class EntityDefaultQueryConfiguration
    {
        public bool AlwaysIncludeCount { get; set; } = true;
        public Dictionary<Type, Func<IDbSet>> Queryables { get; } =
            new Dictionary<Type, Func<IDbSet>>();
        public void ConfigureDefaultGetOperations<TEntity>(Func<IQueryable<TEntity>> queryable)
        {
            if (!Queryables.ContainsKey(typeof(TEntity)))
            {
                Queryables.Add(typeof(TEntity), null);
            }
            Queryables[typeof(TEntity)] = queryable;
        }

        public Func<IDbSet> GetQueryable<TEntity>()
        {
            if (Queryables.ContainsKey(typeof(TEntity)))
            {
                return Queryables[typeof(TEntity)];
            }
            return null;
        }
    }
}