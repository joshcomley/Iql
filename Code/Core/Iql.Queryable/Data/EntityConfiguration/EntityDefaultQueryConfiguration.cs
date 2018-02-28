using System;
using System.Collections.Generic;
using Iql.Queryable.Data.Lists;
using Iql.Queryable.Data.Queryable;

namespace Iql.Queryable.Data.EntityConfiguration
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