using System;
using System.Collections;
using System.Linq;
using Iql.Parsing.Reduction;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Data.Relationships;
using Iql.Queryable.Expressions;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.QueryApplicator.Applicators
{
    public abstract class QueryOperationApplicator<TOperation, TQueryResult, TQueryAdapter>
        : IQueryOperationApplicator<TOperation, TQueryResult, TQueryAdapter>
        where TOperation : IQueryOperation
        where TQueryResult : IQueryResultBase
        where TQueryAdapter : IQueryableAdapterBase
    {
        //public void apply(QueryOperationContext<TEntity> context)
        //{
        //}

        // public resolveExpression(){
        //     let result = new QueryParser().parseRoot<TEntity, ODataParseResult, TApplicator, TQueryResult>(operation.getFilter(), new QueryExpressionAdapterOData<T>());
        //     let filter = result.expression(<TEntity>{});
        // }
        public abstract void Apply<TEntity>(IQueryOperationContext<TOperation, TEntity, TQueryResult, TQueryAdapter> context)
            where TEntity : class;

        public void WithRelationships<TEntity>(IQueryOperationContext<TOperation, TEntity, TQueryResult, TQueryAdapter> context,
            IqlExpression expression,
            Action<IqlPropertyPath, IRelationship> expand)
            where TEntity : class
        {
            var reducer = new IqlReducer();
            var all = reducer.Traverse(expression);
            var anyAlls = all.Where(_ => _ is IqlAnyAllExpression || _ is IqlCountExpression).ToArray();
            for (var i = 0; i < anyAlls.Length; i++)
            {
                var anyAll = anyAlls[i];
                var rootEntityConfiguration = context.DataContext.EntityConfigurationContext.GetEntityByType(context.Queryable.ItemType);
                var iqlPropertyExpression = anyAll.Parent as IqlPropertyExpression;
                var path = IqlPropertyPath.FromPropertyExpression(
                    rootEntityConfiguration,
                    iqlPropertyExpression);
                expand(path, path.Property.Relationship.Relationship);
            }
        }
    }
}