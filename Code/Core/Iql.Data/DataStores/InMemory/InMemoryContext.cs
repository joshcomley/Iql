using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Conversion;
using Iql.Data.Context;
using Iql.Data.Relationships;
using Iql.Entities;
using Iql.Entities.Relationships;
using Iql.Extensions;
using Iql.Parsing.Reduction;
using Iql.Serialization;

namespace Iql.Data.DataStores.InMemory
{

    public class InMemoryContext<TEntity> : IInMemoryContext where TEntity : class
    {
        public Dictionary<string, object> Variables { get; }
            = new Dictionary<string, object>();
        public object GetVariable(string name)
        {
            if (!Variables.ContainsKey(name))
            {
                if (!GlobalContext.GlobalVariables.ContainsKey(name))
                {
                    return null;
                }

                var value = GlobalContext.GlobalVariables[name];
                GlobalContext.GlobalVariables.Remove(name);
                Variables.Add(name, value);

                if (Variables.ContainsKey(name))
                {
                    return Variables[name];
                }
            }

            return null;
        }

        private MethodInfo _addMatchesTypedMethod;
        private MethodInfo AddMatchesTypedMethod
        {
            get
            {
                return _addMatchesTypedMethod = _addMatchesTypedMethod ??
                                                GetType().GetMethod(nameof(AddMatchesTyped));
            }
        }
        private MethodInfo _expandInternalMethod;
        private MethodInfo ExpandInternalMethod
        {
            get
            {
                return _expandInternalMethod = _expandInternalMethod ??
                                               GetType().GetMethod(nameof(ExpandInternal), BindingFlags.NonPublic | BindingFlags.Instance);
            }
        }

        public InMemoryDataStore DataStore { get; }
        public IInMemoryContext Parent { get; }

        public InMemoryContext(InMemoryDataStore dataStore, IInMemoryContext parent = null)
        {
            DataStore = dataStore;
            Parent = parent;
            SourceList = DataStore.DataSet<TEntity>();
        }

#if !TypeScript
        public InMemoryContext<TEntity> Run(MethodCallExpression run)
        {
            var func = (Func<InMemoryContext<TEntity>, InMemoryContext<TEntity>>)Expression.Lambda(run, run.Object as ParameterExpression).Compile();
            func(this);
            return this;
        }
#endif

        public InMemoryContext<TEntity> Where(Expression<Func<TEntity, bool>> predicate, IqlExpression actionFilter)
        {
            var entityConfiguration = DataStore.EntityConfigurationBuilder.GetEntityByType(typeof(TEntity));
            if (actionFilter != null)
            {
                var relationshipExpander = new RelationshipExpander();
                actionFilter = actionFilter.EnsureIsIql();
                WithRelationships(
                    actionFilter,
                    entityConfiguration,
                    (path, relationship) =>
                    {
                        while (true)
                        {
                            var matches = relationshipExpander.FindMatches(
                                ResolveSource(path.Property.Relationship.Relationship.Source.Type).ToList(path.Property.Relationship.Relationship.Source.Type),
                                ResolveSource(path.Property.Relationship.Relationship.Target.Type).ToList(path.Property.Relationship.Relationship.Target.Type),
                                path.Property.Relationship.Relationship,
                                true);
                            if (matches != null)
                            {
                                Enqueue(() => matches.UnassignRelationships());
                            }
                            if (path.Parent == null)
                            {
                                break;
                            }
                            path = path.Parent;
                        }
                    });
            }
            SourceList = SourceList.Where(predicate.Compile());
            return this;
        }

        private readonly List<Action> _queue = new List<Action>();
        private void Enqueue(Action action)
        {
            _queue.Add(action);
        }

        public void Finish()
        {
            foreach (var action in _queue)
            {
                action();
            }
            _queue.Clear();
        }

        private static void WithRelationships(
            IqlExpression expression,
            IEntityConfiguration entityConfiguration,
            Action<IqlPropertyPath, IRelationship> expand
            )
        {
            var topLevelPropertyExpressions = expression.TopLevelPropertyExpressions();
            for (var i = 0; i < topLevelPropertyExpressions.Length; i++)
            {
                var iqlPropertyExpression = topLevelPropertyExpressions[i].Expression as IqlPropertyExpression;
                var path = IqlPropertyPath.FromPropertyExpression(
                    entityConfiguration,
                    iqlPropertyExpression);
                if (path != null && path.RelationshipPath != null)
                {
                    expand(path.RelationshipPath, path.RelationshipPath.Property.Relationship.Relationship);
                }
            }

            var reducer = new IqlReducer();
            var all = reducer.Traverse(expression);
            var anyAlls = all.Where(_ => _.Kind == IqlExpressionKind.Any || _.Kind == IqlExpressionKind.All || _.Kind == IqlExpressionKind.Count).ToArray();
            for (var i = 0; i < anyAlls.Length; i++)
            {
                var anyAll = anyAlls[i];
                var rootEntityConfiguration = entityConfiguration;
                var iqlPropertyExpression = (IqlPropertyExpression)anyAll.Parent;
                var path = IqlPropertyPath.FromPropertyExpression(
                    rootEntityConfiguration,
                    iqlPropertyExpression);
                expand(path, path.Property.Relationship.Relationship);
            }
        }

        public InMemoryContext<TEntity> Expand(IqlExpandExpression expandExpression)
        {
            var path = IqlPropertyPath.FromPropertyExpression(
                DataStore.EntityConfigurationBuilder.EntityType<TEntity>(),
                expandExpression.NavigationProperty);
            var otherSideType = path.Property.Relationship.OtherEnd.EntityConfiguration.Type;
            return (InMemoryContext<TEntity>)ExpandInternalMethod.InvokeGeneric(
                this,
                new object[] { expandExpression, path },
                otherSideType);
        }

        public InMemoryContext<TEntity> OrderBy<TKey>(Func<TEntity, TKey> orderByExpression, bool descending)
        {
            SourceList = descending ? SourceList.OrderByDescending(orderByExpression) : SourceList.OrderBy(orderByExpression);
            return this;
        }

        private InMemoryContext<TEntity> ExpandInternal<TTarget>(
            IqlExpandExpression expandExpression,
            IqlPropertyPath path) where TTarget : class
        {
            var relationship = path.Property.Relationship.Relationship;
            var source = ResolveSource(relationship.Source.Type);
            var target = ResolveSource(relationship.Target.Type);
            var expression = IqlExpressionConversion.DefaultExpressionConverter().ConvertIqlToExpression<TTarget>(
                expandExpression.Query);
            var expandContext = (InMemoryContext<TTarget>)typeof(InMemoryContext<>).ActivateGeneric(
                new object[] { DataStore, this },
                typeof(TTarget));
            var func = expression.Compile();
            expandContext = (InMemoryContext<TTarget>)func.DynamicInvoke(expandContext);
            if (path.Property.Relationship.ThisIsTarget)
            {
                source = expandContext.SourceList;
            }
            else
            {
                target = expandContext.SourceList;
            }
            var matches = new RelationshipExpander().FindMatches(
                source.ToList(relationship.Source.Type),
                target.ToList(relationship.Target.Type),
                relationship,
                true);
            AddMatches(relationship.Source.Type, matches.SourceMatches);
            AddMatches(relationship.Target.Type, matches.TargetMatches);
            // TODO: Apply expandExpression.Query to target list recursively
            // This should take into account filters and order by etc.
            //expandExpression.Query
            return this;
        }

        public void AddMatches(Type type, IList matches)
        {
            if (Parent != null)
            {
                Parent.AddMatches(type, matches);
            }
            else
            {
                AddMatchesTypedMethod.InvokeGeneric(
                    this,
                    new[] { matches },
                    type);
            }
        }

        public double DistanceBetween(IqlPointExpression left, IqlPointExpression right)
        {
            if (left == null || right == null)
            {
                return 0;
            }
            return IqlPointExpression.DistanceBetween(left.X, left.Y, right.X, right.Y);
        }

        public bool Intersects(IqlPointExpression left, IqlPolygonExpression right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            return IqlPointExpression.IntersectsPolygon(left.X, left.Y, right);
        }

        private readonly Dictionary<object, bool> _matched = new Dictionary<object, bool>();

        public readonly Dictionary<Type, IList> AllData = new Dictionary<Type, IList>();
        public void AddMatchesTyped<TEntityMatch>(List<TEntityMatch> matches)
        {
            var type = typeof(TEntityMatch);
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
                var list = (List<TEntityMatch>)AllData[type];
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
            return DataStore.DataSetByType(entityType);
        }

        IEnumerable IInMemoryContext.SourceList
        {
            get => (IList)SourceList;
            set => SourceList = (IList<TEntity>)value;
        }
    }
}