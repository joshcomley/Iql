#if TypeScript
using Iql.Parsing;    
#endif
using System.Linq.Expressions;

namespace Iql.Queryable.Expressions
{
    public interface IIqlToExpressionConverter
    {
        LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
            where TEntity : class;

        string ConvertIqlToExpressionString(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        );
    }
}