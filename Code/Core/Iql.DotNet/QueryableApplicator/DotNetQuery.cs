using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable.Data;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Extensions;

namespace Iql.DotNet.QueryableApplicator
{
    public class DotNetQuery : InMemoryResult<IDotNetQueryResult>, IDotNetQueryResult
    {
        public DotNetQuery(IDataContext dataContext, Type entityType)
        : base(entityType, dataContext)
        {
        }
        public List<Func<IEnumerable, IEnumerable>> Actions { get; } = new List<Func<IEnumerable, IEnumerable>>();
        public override List<TEntity> ApplyOperations<TEntity>()
        {
            var list = (List<TEntity>) this.GetRoot().DataSetByType(typeof(TEntity));
            for (var i = 0; i < Actions.Count; i++)
            {
                var action = Actions[i];
                list = (List<TEntity>)action(list);
            }

            return list;
        }
    }
}