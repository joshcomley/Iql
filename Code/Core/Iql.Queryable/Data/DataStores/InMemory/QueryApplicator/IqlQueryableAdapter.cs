using System;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Extensions;
using Iql.Queryable.Data.DataStores.InMemory.QueryApplicator.Applicators;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.Conversion;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.DataStores.InMemory.QueryApplicator
{
    public class IqlQueryableAdapter : QueryableAdapter<IqlQueryResult, IqlQueryableAdapter>
    {
        static IqlQueryableAdapter()
        {
            QueryExpressionToIqlExpressionTreeMethod =
                typeof(IqlQueryableAdapter).GetMethod(nameof(QueryExpressionToIqlExpressionTree));
        }

        private static MethodInfo QueryExpressionToIqlExpressionTreeMethod { get; set; }

        public IqlQueryableAdapter()
        {
            RegisterApplicator(() => new IqlOrderByOperationApplicator());
            RegisterApplicator(() => new IqlReverseOperationApplicator());
            RegisterApplicator(() => new IqlWhereOperationApplicator());
            RegisterApplicator(() => new IqlExpandOperationApplicator());
        }

        //new JavaScriptExpressionToIqlConverter()
        public static Func<IExpressionConverter> ExpressionConverter { get; set; }

        public static IqlPropertyExpression ExpressionToIqlExpressionTree<T, TProperty>(
            Expression<Func<T, TProperty>> property)
            where T : class
        {
            return (IqlPropertyExpression)QueryExpressionToIqlExpressionTree<T>(
                new ExpressionQueryExpression(property, QueryExpressionType.NonBinary)
            );
        }

        public static IqlPropertyExpression LambdaExpressionToIqlExpressionTree(
            LambdaExpression property,
            Type entityType)
        {
            return (IqlPropertyExpression) QueryExpressionToIqlExpressionTreeMethod.InvokeGeneric(
                    null,
                    new object[] { new ExpressionQueryExpression(property, QueryExpressionType.NonBinary) }, 
                    entityType);
        }

        public static IqlExpression QueryOperationToIqlExpression<T>(
            IExpressionQueryOperation operation)
            where T : class
        {
            if (operation.Expression == null)
            {
                var queryExpression = operation.GetExpression();
#if TypeScript
                queryExpression.EvaluateContext = queryExpression.EvaluateContext ?? operation.EvaluateContext;
#endif
                return QueryExpressionToIqlExpressionTree<T>(
                    queryExpression
                );
            }
            return operation.Expression;
        }

        public static IqlExpression QueryExpressionToIqlExpressionTree<T>(
            QueryExpression expression)
            where T : class
        {
            var expressionResult = ExpressionConverter().ConvertQueryExpressionToIql<T>(
                expression
            );
            return expressionResult.Expression;
        }

        public override IqlQueryResult NewQueryData<TEntity>(IQueryable<TEntity> queryable)
        {
            return new IqlQueryResult();
        }

        public override IqlQueryResult ToQueryResult<TEntity>(IQueryable<TEntity> queryable, IqlQueryResult data)
        {
            return data;
        }
        //    return new IqlData();
        //{

        //public override IqlData NewQueryData<TEntity>(Queryable<TEntity> queryable)
        //}
        //    return new IqlQueryResult<T>();
        //{
        //public override IqlQueryResult<T> ToQueryResult<TEntity>(Queryable<TEntity> queryable, IqlData data)
        //}
    }
}