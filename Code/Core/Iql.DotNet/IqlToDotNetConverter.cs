using System.Linq.Expressions;
using Iql.DotNet.IqlToDotNet;
using Iql.DotNet.IqlToDotNetString;
#if TypeScript
using Iql.Parsing;
#endif
using Iql.Queryable.Expressions;

namespace Iql.DotNet
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
            return dotNetExpression.ToLambda();
        }

        public string ConvertIqlToExpressionString(IqlExpression iql)
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
    }
}