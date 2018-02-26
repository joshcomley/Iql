using System.Linq.Expressions;
using Iql.DotNet.IqlToDotNet;
using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.DotNet
{
    public class DotNetExpressionConverter : IExpressionConverter
    {
        public ExpressionResult<IqlExpression> ConvertExpressionToIql<TEntity>(QueryExpression filter) where TEntity : class
        {
            var whereQueryExpression = filter.TryFlatten<TEntity>() as ExpressionQueryExpressionBase;
            var lambdaExpression = whereQueryExpression.GetExpression();
            return new ExpressionResult<IqlExpression>(
                ExpressionToIqlExpressionParser<TEntity>.Parse(
                    lambdaExpression
#if TypeScript
                    , filter.EvaluateContext
#endif
                    )
            );
        }

        public LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression iql) where TEntity : class
        {
            return new IqlToDotNetConverter().ConvertIqlToExpression<TEntity>(iql);
        }
    }
}