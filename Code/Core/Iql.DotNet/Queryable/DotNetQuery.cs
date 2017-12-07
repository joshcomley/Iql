using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Operations;

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

        public string GetDataSetObjectName(Type type)
        {
            return "dataSet_" + type.Name;
        }

        public List<Func<IEnumerable, IEnumerable>> Actions { get; } = new List<Func<IEnumerable, IEnumerable>>();
        public bool HasKey { get; set; }
        public CompositeKey Key { get; set; }

        public IList DataSetByType(Type type)
        {
            var sourceSet = DataContext.GetConfiguration<InMemoryDataStoreConfiguration>()
                .GetSourceByType(type);
            var cloneSet = sourceSet.CloneAs(DataContext, type, RelationshipCloneMode.DoNotClone);
            return cloneSet;
        }

        public override List<T> ToList()
        {
            var list = (IEnumerable<T>)DataSetByType(typeof(T));
            foreach (var action in Actions)
            {
                list = (IEnumerable<T>) action(list);
            }
            var clone = list.ToList().CloneAs<IEnumerable<T>>(DataContext, typeof(T), RelationshipCloneMode.Full);
            return new DbList<T>(clone);
        }
    }
}