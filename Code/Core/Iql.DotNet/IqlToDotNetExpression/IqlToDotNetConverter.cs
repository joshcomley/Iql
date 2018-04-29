using System;
using System.Linq.Expressions;
using Iql.DotNet.IqlToDotNetString;
using Iql.Queryable.Expressions.Conversion;
#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.DotNet.IqlToDotNetExpression
{
    public class IqlToDotNetConverter : IIqlToExpressionConverter
    {
        public LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression iql
#if TypeScript
                , EvaluateContext evaluateContext
#endif
        ) where TEntity : class
        {
            var adapter = new DotNetIqlExpressionAdapter("entity");
            var parser = new DotNetIqlParserInstance(adapter, typeof(TEntity));
            parser.IsFilter = true;
            var dotNetExpression = parser.Parse(iql
#if TypeScript
                , evaluateContext
#endif
            );
            return parser.ConvertToLambda ? dotNetExpression.ToLambda() : dotNetExpression.Expression as LambdaExpression;
        }

        public string ConvertIqlToExpressionStringByType(IqlExpression iql,
            Type rootEntityType
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
        {
            var adapter = new DotNetStringIqlExpressionAdapter("entity");
            var parser = new DotNetStringIqlParserInstance(adapter);
            parser.IsFilter = true;
            var dotNetExpression = parser.Parse(iql
#if TypeScript
                , evaluateContext
#endif
            );
            return $"{adapter.RootVariableName} => {dotNetExpression.Expression}";
        }

        public string ConvertIqlToExpressionString<TEntity>(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
        {
            return ConvertIqlToExpressionStringByType(expression, typeof(TEntity)
#if TypeScript
            , evaluateContext
#endif
            );
        }
    }
}