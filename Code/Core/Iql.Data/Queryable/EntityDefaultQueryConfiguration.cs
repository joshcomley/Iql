using System;
using System.Collections.Generic;
using Iql.Data.Context;
using Iql.Data.Lists;

namespace Iql.Data.Queryable
{
    public class EntityDefaultQueryConfiguration
    {
        public bool AlwaysIncludeCount { get; set; } = true;
        public Dictionary<Type, Func<IDbQueryable>> Queryables { get; } =
            new Dictionary<Type, Func<IDbQueryable>>();
        public void ConfigureDefaultGetOperations<TEntity>(Func<DbQueryable<TEntity>> queryable) 
            where TEntity : class
        {
            if (!Queryables.ContainsKey(typeof(TEntity)))
            {
                Queryables.Add(typeof(TEntity), null);
            }
            Queryables[typeof(TEntity)] = queryable;
        }

        public Func<IDbQueryable> GetQueryable<TEntity>()
        {
            if (Queryables.ContainsKey(typeof(TEntity)))
            {
                return Queryables[typeof(TEntity)];
            }
            return null;
        }
    }
}