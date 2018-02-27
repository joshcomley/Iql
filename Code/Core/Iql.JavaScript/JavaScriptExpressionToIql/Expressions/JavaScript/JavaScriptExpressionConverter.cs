using System.Linq.Expressions;
using Iql.JavaScript.IqlToJavaScript.Parsers;
#if TypeScript
using Iql.Parsing;
#endif
using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript
{
    public class JavaScriptExpressionConverter : IExpressionConverter
    {
        public ExpressionResult<IqlExpression> ConvertExpressionToIql<TEntity>(QueryExpression filter) where TEntity : class
        {
            return new JavaScriptExpressionToIqlConverter().ConvertExpressionToIql<TEntity>(filter);
        }

        public LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        ) where TEntity : class
        {
            var javascript = JavaScriptIqlParser.GetJavaScript(expression
#if TypeScript
            , evaluateContext
#endif
            );
            return (LambdaExpression)Evaluator.Eval(javascript.Expression);
        }
    }
}