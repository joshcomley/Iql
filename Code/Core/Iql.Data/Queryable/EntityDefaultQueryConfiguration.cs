using System;
using System.Collections.Generic;
using Iql.Data.Context;
using Iql.Data.Lists;

namespace Iql.Data.Queryable
{
    public class EntityDefaultQueryConfiguration
    {
        public bool AlwaysIncludeCount { get; set; } = true;
        private Dictionary<Type, Func<IDbQueryable, IDbQueryable>> _queryables;

        public Dictionary<Type, Func<IDbQueryable, IDbQueryable>> Queryables =>
            _queryables = _queryables ?? new Dictionary<Type, Func<IDbQueryable, IDbQueryable>>();

        public void ConfigureDefaultGetOperations<TEntity>(Func<DbQueryable<TEntity>, DbQueryable<TEntity>> queryable)
            where TEntity : class
        {
            if (!Queryables.ContainsKey(typeof(TEntity)))
            {
                Queryables.Add(typeof(TEntity), null);
            }

            Queryables[typeof(TEntity)] = _ => queryable((DbQueryable<TEntity>)_);
        }

        public Func<IDbQueryable, IDbQueryable> GetQueryable<TEntity>()
        {
            return GetQueryableByType(typeof(TEntity));
        }

        public Func<IDbQueryable, IDbQueryable> GetQueryableByType(Type type)
        {
            if (Queryables.ContainsKey(type))
            {
                return Queryables[type];
            }

            return null;
        }
    }
}