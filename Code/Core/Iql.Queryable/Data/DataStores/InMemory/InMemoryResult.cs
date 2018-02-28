using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Iql.Extensions;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Data.Relationships;
using Iql.Queryable.Extensions;
using Iql.Queryable.QueryApplicator;

namespace Iql.Queryable.Data.DataStores.InMemory
{
    public abstract class InMemoryResult<T> : QueryResult<T>, IInMemoryResult
        where T : IQueryResultBase
    {
        private readonly Dictionary<Type, IList> _clonedSets = new Dictionary<Type, IList>();

        private readonly Dictionary<object, bool> _matched = new Dictionary<object, bool>();

        private readonly Dictionary<Type, IList> _matches = new Dictionary<Type, IList>();
        private RelationshipExpander _relationshipExpander;
        private MethodInfo _getResultsTypedMethod;
        private MethodInfo _addMatchesTypedMethod;

        public InMemoryResult(Type entityType, IDataContext dataContext)
        {
            EntityType = entityType;
            DataContext = dataContext;
            Configuration = DataContext.GetConfiguration<InMemoryDataStoreConfiguration>();
        }

        public Type EntityType { get; }
        public IDataContext DataContext { get; }

        public MethodInfo GetResultsTypedMethod
        {
            get
            {
                return _getResultsTypedMethod = _getResultsTypedMethod ??
              GetType().GetMethod(nameof(GetResultsTyped));
            }
        }

        public MethodInfo AddMatchesTypedMethod
        {
            get
            {
                return _addMatchesTypedMethod = _addMatchesTypedMethod ??
              GetType().GetMethod(nameof(AddMatchesTyped));
            }
        }

        public InMemoryDataStoreConfiguration Configuration { get; set; }

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

        public InMemoryQueryResult GetResults()
        {
            return (InMemoryQueryResult)GetResultsTypedMethod
                .InvokeGeneric(this, null, EntityType);
        }

        public void AddMatches(Type type, IList matches)
        {
            AddMatchesTypedMethod.InvokeGeneric(
                this,
                new[] { matches },
                type);
        }

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

        public InMemoryQueryResult GetResultsTyped<T1>()
        {
            //var list = Configuration.GetSource<T>();
            var list = ApplyOperations<T1>();
            //var clone = list.CloneAs(DataContext, typeof(T), RelationshipCloneMode.Full);
            //return clone;
            AddMatchesTyped(list);
            return new InMemoryQueryResult(_matches, list);
        }

        public abstract List<TEntity> ApplyOperations<TEntity>();

        public IList DataSetByType(Type type)
        {
            //return DataContext.GetConfiguration<InMemoryDataStoreConfiguration>()
            //    .GetSourceByType(type);
            if (!_clonedSets.ContainsKey(type))
            {
                var sourceSet = Configuration.GetSourceByType(type);
                var cloneSet = sourceSet; //.CloneAs(DataContext, type, RelationshipCloneMode.DoNotClone);
                _clonedSets.Add(type, cloneSet);
            }

            return _clonedSets[type];
        }
    }
}