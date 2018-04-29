using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Extensions;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.Relationships;
using Iql.Queryable.Expressions;

namespace Iql.Queryable.Data.DataStores.InMemory
{
    public interface IInMemoryContext
    {
        IEnumerable SourceList { get; set; }
        IEnumerable ResolveSource(Type entityType);
    }

    public class InMemoryContext<TEntity> : IInMemoryContext where TEntity : class
    {
        private MethodInfo _addMatchesTypedMethod;
        private MethodInfo AddMatchesTypedMethod
        {
            get
            {
                return _addMatchesTypedMethod = _addMatchesTypedMethod ??
                                                GetType().GetMethod(nameof(AddMatchesTyped));
            }
        }
        public IDataContext DataContext { get; }

        public InMemoryContext(IDataContext dataContext)
        {
            DataContext = dataContext;
            SourceList =
                DataContext.GetConfiguration<InMemoryDataStoreConfiguration>().GetSource<TEntity>();
        }

        public InMemoryContext<TEntity> Run(MethodCallExpression run)
        {
            var func = (Func<InMemoryContext<TEntity>, InMemoryContext<TEntity>>)Expression.Lambda(run, run.Object as ParameterExpression).Compile();
            func(this);
            return this;
        }

        public InMemoryContext<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            SourceList = SourceList.Where(predicate.Compile());
            return this;
        }

        public InMemoryContext<TEntity> Expand(IqlExpandExpression expandExpression)
        {
            var path = IqlPropertyPath.FromPropertyExpression(DataContext.EntityConfigurationContext.EntityType<TEntity>(), expandExpression.NavigationProperty);
            var relationship = path.Property.Relationship.Relationship;
            var source = (IList)ResolveSource(relationship.Source.Type);
            var target = (IList)ResolveSource(relationship.Target.Type);
            var matches = new RelationshipExpander().FindMatches(
                source,
                target,
                relationship,
                true);
            AddMatches(relationship.Source.Type, matches.SourceMatches);
            AddMatches(relationship.Target.Type, matches.TargetMatches);
            return this;
        }

        public void AddMatches(Type type, IList matches)
        {
            AddMatchesTypedMethod.InvokeGeneric(
                this,
                new[] { matches },
                type);
        }

        private readonly Dictionary<object, bool> _matched = new Dictionary<object, bool>();

        public readonly Dictionary<Type, IList> AllData = new Dictionary<Type, IList>();
        public void AddMatchesTyped<TEntity>(List<TEntity> matches)
        {
            var type = typeof(TEntity);
            if (!AllData.ContainsKey(type))
            {
                AllData.Add(type, matches);
                foreach (var match in matches)
                {
                    _matched.Add(match, true);
                }
            }
            else
            {
                var list = (List<TEntity>)AllData[type];
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
        public IEnumerable<TEntity> SourceList { get; set; }

        public IEnumerable ResolveSource(Type entityType)
        {
            return DataContext.GetConfiguration<InMemoryDataStoreConfiguration>().GetSourceByType(entityType);
        }

        IEnumerable IInMemoryContext.SourceList
        {
            get => (IList) SourceList;
            set => SourceList = (IList<TEntity>) value;
        }
    }
}