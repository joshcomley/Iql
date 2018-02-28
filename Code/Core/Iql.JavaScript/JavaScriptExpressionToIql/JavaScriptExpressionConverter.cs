using System.Linq.Expressions;
using Iql.JavaScript.IqlToJavaScriptExpression.Parsers;
using Iql.JavaScript.QueryableApplicator;
using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.Conversion;
using Iql.Queryable.Expressions.QueryExpressions;
#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.JavaScript.JavaScriptExpressionToIql
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
            return (LambdaExpression)Evaluator.Eval(ConvertIqlToJavaScript(expression
#if TypeScript
            , evaluateContext
#endif
            ).Expression);
        }

        public string ConvertIqlToExpressionString(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
        {
            var javascript = ConvertIqlToJavaScript(expression
#if TypeScript
            , evaluateContext
#endif
                );
            return $"function({javascript.RootVariableName}) {{ return {javascript.Expression}; }}";
        }

        public string ConvertIqlToTypeScriptExpressionString(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var javascript = ConvertIqlToJavaScript(expression
#if TypeScript
            , evaluateContext
#endif
            );
            return $"{javascript.RootVariableName} => {javascript.Expression}";
        }

        private static JavaScriptExpression ConvertIqlToJavaScript(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
        {
            var javascript = JavaScriptIqlParser.GetJavaScript(expression
#if TypeScript
            , evaluateContext
#endif
            );
            return javascript;
        }
    }
}