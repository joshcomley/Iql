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
    public class JavaScriptExpressionConverter : ExpressionConverterBase
    {
        public override ExpressionResult<IqlExpression> ConvertLambdaExpressionToIql<TEntity>(LambdaExpression lambdaExpression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
            )
        {
            return
                new JavaScriptExpressionToIqlConverter().ConvertLambdaExpressionToIql<TEntity>(
                    lambdaExpression
#if TypeScript
                    , evaluateContext
#endif
                );
        }
        public override ExpressionResult<IqlExpression> ConvertQueryExpressionToIql<TEntity>(QueryExpression filter)
        {
            return new JavaScriptExpressionToIqlConverter().ConvertQueryExpressionToIql<TEntity>(filter);
        }

        public override LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
        {
            return (LambdaExpression)Evaluator.Eval(ConvertIqlToJavaScript(expression
#if TypeScript
            , evaluateContext
#endif
            ).Expression);
        }

        public override string ConvertIqlToExpressionString(IqlExpression expression
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