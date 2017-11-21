using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Data.Tracking;

namespace Iql.DotNet.Queryable
{
    public class DotNetQuery<T> : QueryResult<T>, IDotNetQueryResult
        where T : class
    {
        public DotNetQuery(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public IDataContext DataContext { get; }

        public List<Func<IList, IList>> Actions { get; } = new List<Func<IList, IList>>();

        public IList DataSet(string name)
        {
            var sourceSet = DataContext.GetConfiguration<InMemoryDataStoreConfiguration>()
                .GetSourceByName(name);
            var cloneSet = sourceSet.Clone();
            return cloneSet;
        }

        public override List<T> ToList()
        {
            var list = DataSet(typeof(T).Name);
            foreach (var action in Actions)
            {
                list = action(list);
            }
            return (List<T>) list;
        }
    }
}