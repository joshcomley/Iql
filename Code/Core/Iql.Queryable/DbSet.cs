using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Extensions;
using Iql.Parsing;
using Iql.Queryable.Data;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public class DbSet<T, TKey> : Queryable<T, DbSet<T, TKey>>, IDbSet, IDbSetOperations<T, TKey> where T : class
    {
        public DbSet(EntityConfigurationBuilder configuration, Func<IDataStore> dataStoreGetter,
            EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(evaluateContext)
        {
            Configuration = configuration;
            DataContext = dataContext;
            DataStoreGetter = dataStoreGetter;
        }

        public Func<IDataStore> DataStoreGetter { get; set; }
        public IDataContext DataContext { get; set; }
        public EntityConfigurationBuilder Configuration { get; set; }

        //public IqlExpression ToIql(IExpressionQueryOperation operation, IDataContext dataContext)
        //{
        //    var adapter = new IqlQueryableAdapter();
        //    var applicator = adapter
        //        .ResolveApplicator(operation);
        //    var newQueryData = adapter.NewQueryData(this);
        //    ApplyOperation(operation, dataContext, newQueryData, applicator);
        //    return new IqlReducer(
        //            // TODO: Add reducer registry
        //            //queryOperation.getExpression().evaluateContext || this.evaluateContext
        //        )
        //        .ReduceStaticContent(operation.Expression);
        //}

        //public TQueryResult ToQueryWithAdapter<TQueryResult>
        //(
        //    IQueryableAdapter<TQueryResult> adapter
        //)
        //    where TQueryResult : IQueryResultBase
        //{
        //    return (TQueryResult)ToQueryWithAdapterBase(adapter);
        //}

        //public IQueryResultBase ToQueryWithAdapterBase
        //(
        //    IQueryableAdapterBase adapter
        //)
        //{
        //    // if (!adapter) {
        //    //     if (!QueryableBase.DefaulTQueryAdapterResolver) {
        //    //         QueryableBase.DefaulTQueryAdapterResolver = () => new JavaScriptQueryableAdapter();
        //    //     }
        //    //     adapter = QueryableBase.DefaulTQueryAdapterResolver();
        //    // }
        //    //var adapter = new adapterCtor();
        //    adapter.Context = DataContext;
        //    adapter.Begin(DataContext);
        //    var data = adapter.NewQueryData(this);
        //    Operations.ForEach(operation =>
        //    {
        //        var applicator = adapter.ResolveApplicator(operation);
        //        if (operation is OrderByOperation ||
        //            operation is WhereOperation ||
        //            operation is IExpandOperation)
        //        {
        //            ToIql(operation as IExpressionQueryOperation, DataContext);
        //        }
        //        if (operation is IExpandOperation)
        //        {
        //            var expand = operation as IExpandOperation;
        //            expand.ExpandDetails = new List<ExpandDetail>();
        //            ResolveExpand(
        //                DataContext,
        //                expand,
        //                expand.Expression as IqlPropertyExpression,
        //                typeof(T));
        //        }
        //        if (applicator == null)
        //        {
        //            throw new Exception("Operation not supported: " + operation.ToString());
        //        }
        //        ApplyOperation(operation as IExpressionQueryOperation,
        //            DataContext,
        //            data,
        //            applicator);
        //    });
        //    return data;
        //}

        //private void ApplyOperation(IExpressionQueryOperation operation, IDataContext dataContext,
        //    IQueryResultBase newQueryData, IQueryOperationApplicatorBase applicator)
        //{
        //    var contextArgs = new List<object>();
        //    if (Platform.Name == "JavaScript")
        //    {
        //        contextArgs.Add(null);
        //        contextArgs.Add(null);
        //        contextArgs.Add(null);
        //    }
        //    contextArgs.Add(dataContext);
        //    contextArgs.Add(operation);
        //    contextArgs.Add(newQueryData);
        //    contextArgs.Add(this);
        //    var context = Activator.CreateInstance(
        //        typeof(QueryOperationContext<,,>)
        //            .MakeGenericType(typeof(T), operation.GetType(), newQueryData.GetType()),
        //           contextArgs.ToArray()
        //        );
        //    var name = nameof(IQueryOperationApplicator<IExpressionQueryOperation, IQueryResultBase>.Apply);
        //    var method = applicator.GetType()
        //        .GetRuntimeMethods()
        //        .Single(m => m.Name == name)
        //        .MakeGenericMethod(typeof(T));
        //    var args = new List<object>();
        //    if (Platform.Name == "JavaScript")
        //    {
        //        args.Add(null);
        //    }
        //    args.Add(context);
        //    method.Invoke(applicator, args.ToArray());
        //}

        //private Type ResolveExpand(
        //    IDataContext dataContext,
        //    IExpandOperation operation,
        //    IqlPropertyExpression expression,
        //    Type typeConstructor,
        //    int depth = 0
        //)
        //{
        //    if (expression.Parent.Type != IqlExpressionType.RootReference)
        //    {
        //        typeConstructor = ResolveExpand(
        //            dataContext,
        //            operation,
        //            expression.Parent as IqlPropertyExpression,
        //            typeConstructor,
        //            depth + 1);
        //    }
        //    var propertyToExpand = expression;
        //    var entityConfiguration = dataContext.EntityConfigurationContext.GetEntityByName(
        //        typeConstructor.Name
        //    );
        //    for (var i = 0; i < entityConfiguration.Relationships.Count; i++)
        //    {
        //        var relationship = entityConfiguration.Relationships[i];
        //        if (relationship.SourceProperty.PropertyName == propertyToExpand.PropertyName ||
        //            relationship.TargetProperty.PropertyName == propertyToExpand.PropertyName)
        //        {
        //            var detail = new ExpandDetail(
        //                dataContext.AsDbSetByType(relationship.SourceType),
        //                dataContext.AsDbSetByType(relationship.TargetType),
        //                relationship);
        //            // We have our relationship
        //            operation.ExpandDetails.Add(detail);
        //            if (depth == 0)
        //            {
        //                detail.TargetQueryable = operation.ApplyQuery(detail.TargetQueryable);
        //            }
        //            return relationship.TargetType;
        //        }
        //    }
        //    return null;
        //}

        public async Task<GetSingleResult<T>> WithKey(TKey key)
        {
            return await Then(new WithKeyOperation(key)).SingleOrDefault();
        }

        public async Task<GetSingleResult<T>> Single(Expression<Func<T, bool>> expression = null,
            EvaluateContext evaluateContext = null)
        {
            var result = await UseWhereIfExists(expression, evaluateContext)
                .ToListWithResponse();
            return ResolveSingle(result);
        }

        public async Task<GetSingleResult<T>> SingleQuery(WhereQueryExpression<T> expression,
            EvaluateContext evaluateContext = null)
        {
            var result = await UseWhereQueryIfExists(expression, evaluateContext)
                .ToListWithResponse();
            return ResolveSingle(result);
        }

        public async Task<GetSingleResult<T>> SingleOrDefault(Expression<Func<T, bool>> expression = null,
            EvaluateContext evaluateContext = null)
        {
            var result = await UseWhereIfExists(expression, evaluateContext)
                .ToListWithResponse();
            return ResolveSingleOrDefault(result);
        }

        public async Task<GetSingleResult<T>> SingleOrDefaultQuery(WhereQueryExpression<T> expression,
            EvaluateContext evaluateContext = null)
        {
            var result = await UseWhereQueryIfExists(expression, evaluateContext)
                .ToListWithResponse();
            return ResolveSingleOrDefault(result);
        }

        public async Task<GetSingleResult<T>> First(Expression<Func<T, bool>> expression = null,
            EvaluateContext evaluateContext = null)
        {
            var result = await UseWhereIfExists(expression, evaluateContext)
                .ToListWithResponse();
            return ResolveFirst(result);
        }

        public async Task<GetSingleResult<T>> FirstQuery(WhereQueryExpression<T> expression,
            EvaluateContext evaluateContext = null)
        {
            var result = await UseWhereQueryIfExists(expression, evaluateContext)
                .ToListWithResponse();
            return ResolveFirst(result);
        }

        public async Task<GetSingleResult<T>> FirstOrDefault(Expression<Func<T, bool>> expression = null,
            EvaluateContext evaluateContext = null)
        {
            var result = await UseWhereIfExists(expression, evaluateContext)
                .ToListWithResponse();
            return ResolveFirstOrDefault(result);
        }

        public async Task<GetSingleResult<T>> FirstOrDefaultQuery(WhereQueryExpression<T> expression,
            EvaluateContext evaluateContext = null)
        {
            var result = await UseWhereQueryIfExists(expression, evaluateContext)
                .ToListWithResponse();
            return ResolveFirstOrDefault(result);
        }

        public async Task<List<T>> ToList()
        {
            var result = await ToListWithResponse();
            return result?.Data;
        }

        public async Task<GetDataResult<T>> ToListWithResponse()
        {
            return await DataContext.DataStore.Get(new GetDataOperation<T>(this, DataContext));
        }

        public AddEntityResult<T> Add(T entity)
        {
            return DataContext.DataStore.Add(
                new AddEntityOperation<T>(entity, DataContext));
        }

        public UpdateEntityResult<T> Update(T entity)
        {
            return DataContext.DataStore.Update(
                new UpdateEntityOperation<T>(entity, DataContext));
        }

        public DeleteEntityResult<T> Delete(T entity)
        {
            return DataContext.DataStore.Delete(
                new DeleteEntityOperation<T>(entity, DataContext));
        }

        public async Task<SaveChangesResult> SaveChanges(T entity)
        {
            return await DataContext.DataStore.SaveChanges(
                new SaveChangesOperation(DataContext));
        }

        private DbSet<T, TKey> UseWhereIfExists(Expression<Func<T, bool>> expression,
            EvaluateContext evaluateContext = null)
        {
            var queryable = this;
            if (expression != null)
            {
                queryable = Where(expression, evaluateContext);
            }
            return queryable;
        }

        private DbSet<T, TKey> UseWhereQueryIfExists(QueryExpression expression, EvaluateContext evaluateContext = null)
        {
            var queryable = this;
            if (expression != null)
            {
                queryable = WhereQuery(expression, evaluateContext);
            }
            return queryable;
        }

        private static GetSingleResult<T> ResolveSingle(GetDataResult<T> result)
        {
            if (result.Success)
            {
                if (result.Data.Count < 1)
                {
                    throw new Exception("No entities returned for \"single\" call");
                }
                if (result.Data.Count > 1)
                {
                    throw new Exception("More than one entity returned for \"single\" call");
                }
            }
            return new GetSingleResult<T>(
                result.Data[0],
                result.Operation,
                result.Success);
        }

        private static GetSingleResult<T> ResolveSingleOrDefault(GetDataResult<T> result)
        {
            T data;
            if (result.Data.Count < 1)
            {
                data = null;
            }
            else if (result.Data.Count > 1)
            {
                throw new Exception("More than one entity returned for \"single\" call");
            }
            else
            {
                data = result.Data[0];
            }
            return new GetSingleResult<T>(
                data,
                result.Operation,
                result.Success);
        }

        private static GetSingleResult<T> ResolveFirst(GetDataResult<T> result)
        {
            if (result.Data.Count < 1)
            {
                throw new Exception("No entities returned for \"first\" call");
            }
            return new GetSingleResult<T>(
                result.Data[0],
                result.Operation,
                result.Success);
        }

        private static GetSingleResult<T> ResolveFirstOrDefault(GetDataResult<T> result)
        {
            var data = result.Data.Count < 1 ? null : result.Data[0];
            return new GetSingleResult<T>(
                data,
                result.Operation,
                result.Success);
        }

        protected override DbSet<T, TKey> New()
        {
            var dbSet = new DbSet<T, TKey>(
                Configuration,
                DataStoreGetter,
                EvaluateContext,
                DataContext);
            return dbSet;
        }
    }
}