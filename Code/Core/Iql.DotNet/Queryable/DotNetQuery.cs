using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.DotNet.Queryable
{
    public class DotNetQuery<T> : QueryResult<T, IDotNetQueryResult>, IDotNetQueryResult
        where T : class
    {
        public DotNetQuery(IDataContext dataContext)
        {
            DataContext = dataContext;
            Configuration = DataContext.GetConfiguration<InMemoryDataStoreConfiguration>();
        }

        public InMemoryDataStoreConfiguration Configuration { get; set; }

        public IDataContext DataContext { get; }

        public string GetDataSetObjectName(Type type)
        {
            return "dataSet_" + type.Name;
        }

        public List<Func<IEnumerable, IEnumerable>> Actions { get; } = new List<Func<IEnumerable, IEnumerable>>();
        public bool HasKey { get; set; }
        public CompositeKey Key { get; set; }

        private readonly Dictionary<Type, IList> _clonedSets = new Dictionary<Type, IList>();

        public IList DataSetByType(Type type)
        {
            //return DataContext.GetConfiguration<InMemoryDataStoreConfiguration>()
            //    .GetSourceByType(type);
            if (!_clonedSets.ContainsKey(type))
            {
                var sourceSet = Configuration.GetSourceByType(type);
                var cloneSet = sourceSet;//.CloneAs(DataContext, type, RelationshipCloneMode.DoNotClone);
                _clonedSets.Add(type, cloneSet);
            }
            return _clonedSets[type];
        }

        public override List<T> ToList()
        {
            //var list = Configuration.GetSource<T>();
            var list = (List<T>)this.GetRoot<IDotNetQueryResult>().DataSetByType(typeof(T));
            for (var i = 0; i < Actions.Count; i++)
            {
                var action = Actions[i];
                list = (List<T>)action(list);
            }
            //var clone = list.CloneAs(DataContext, typeof(T), RelationshipCloneMode.Full);
            //return clone;
            return list;
        }
    }
}