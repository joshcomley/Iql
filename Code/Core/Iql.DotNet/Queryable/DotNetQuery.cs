using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Iql.Extensions;
using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Extensions;
using Iql.Queryable.Native;
using Iql.Queryable.Operations;

namespace Iql.DotNet.Queryable
{
    public class DotNetQuery : QueryResult<IDotNetQueryResult>, IDotNetQueryResult
    {
        public RelationshipExpander RelationshipExpander
        {
            get
            {
                if (_relationshipExpander == null)
                {
                    var root = this.GetRoot();
                    _relationshipExpander = root == this
                        ? new RelationshipExpander()
                        : root.RelationshipExpander;
                }
                return _relationshipExpander;
            }
        }

        public DotNetQuery(IDataContext dataContext, Type entityType)
        {
            DataContext = dataContext;
            EntityType = entityType;
            Configuration = DataContext.GetConfiguration<InMemoryDataStoreConfiguration>();
        }

        public InMemoryDataStoreConfiguration Configuration { get; set; }

        public IDataContext DataContext { get; }
        public Type EntityType { get; }

        public string GetDataSetObjectName(Type type)
        {
            return "dataSet_" + type.Name;
        }

        static DotNetQuery()
        {
            AddMatchesTypedMethod = typeof(DotNetQuery)
                .GetMethod(nameof(AddMatchesTyped));
            ToListTypedMethod = typeof(DotNetQuery)
                .GetMethod(nameof(ToListTyped));
        }

        public static MethodInfo ToListTypedMethod { get; set; }

        public static MethodInfo AddMatchesTypedMethod { get; set; }

        public List<Func<IEnumerable, IEnumerable>> Actions { get; } = new List<Func<IEnumerable, IEnumerable>>();
        public bool HasKey { get; set; }
        public CompositeKey Key { get; set; }

        private readonly Dictionary<Type, IList> _clonedSets = new Dictionary<Type, IList>();
        private RelationshipExpander _relationshipExpander;

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

        private readonly Dictionary<Type, IList> _matches = new Dictionary<Type, IList>();
        public void AddMatches(Type type, IList matches)
        {
            AddMatchesTypedMethod.InvokeGeneric(
                this, new[] { matches }, type);
        }

        private readonly Dictionary<object, bool> _matched = new Dictionary<object, bool>();
        public void AddMatchesTyped<TEntity>(List<TEntity> matches)
        {
            var type = typeof(TEntity);
            if (!_matches.ContainsKey(type))
            {
                _matches.Add(type, matches);
                foreach (var match in matches)
                {
                    _matched.Add(match, true);
                }
            }
            else
            {
                var list = (List<TEntity>)_matches[type];
                foreach (var match in matches)
                {
                    if (!_matched.ContainsKey(match))
                    {
                        _matched.Add(match, true);
                        list.Add(match);
                    }
                }
            }
        }

        public Dictionary<Type, IList> GetResults()
        {
            return (Dictionary<Type, IList>) ToListTypedMethod
                .InvokeGeneric(this, null, EntityType);
        }

        public Dictionary<Type, IList> ToListTyped<T>()
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
            AddMatchesTyped(list);
            return _matches;
        }
    }
}