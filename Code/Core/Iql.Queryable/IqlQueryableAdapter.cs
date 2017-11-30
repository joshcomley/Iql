using System;
using System.Linq.Expressions;
using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public class IqlQueryableAdapter : QueryableAdapter<IqlQueryResult>
    {
        public IqlQueryableAdapter()
        {
            RegisterApplicator(() => new IqlOrderByOperationApplicator());
            RegisterApplicator(() => new IqlReverseOperationApplicator());
            RegisterApplicator(() => new IqlWhereOperationApplicator());
            RegisterApplicator(() => new IqlExpandOperationApplicator());
        }

        //new JavaScriptExpressionToIqlConverter()
        public static Func<IExpressionToIqlConverter> ExpressionConverter { get; set; }
        public static Func<IIqlToNativeConverter> IqlToNativeConverter { get; set; }

        public static IqlExpression ExpressionToIqlExpressionTree<T, TProperty>(
            Expression<Func<T, TProperty>> property)
            where T : class
        {
            return QueryExpressionToIqlExpressionTree<T>(
                new ExpressionQueryExpression<T, TProperty>(property, QueryExpressionType.NonBinary)
            );
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
            var expressionResult = ExpressionConverter().Parse<T>(
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