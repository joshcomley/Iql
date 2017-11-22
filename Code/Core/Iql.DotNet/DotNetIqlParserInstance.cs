using System;
using System.Linq.Expressions;
using Iql.DotNet.IqlToDotNet;
using Iql.Parsing;
using Iql.Parsing.Reduction;

namespace Iql.DotNet
{
    public class DotNetIqlParserInstance : ActionParserInstance<DotNetIqlData, DotNetIqlExpressionAdapter, Expression, DotNetOutput>
    {
        public DotNetIqlParserInstance(DotNetIqlExpressionAdapter adapter, Type rootEntityType) : base(adapter, rootEntityType)
        {
            RootEntity = System.Linq.Expressions.Expression.Parameter(rootEntityType, "entity");
        }

        public ParameterExpression RootEntity { get; }

        public override DotNetOutput Parse(IqlExpression expression)
        {
            return new DotNetOutput(ParseAsExpression(expression));
        }

        private Expression ParseAsExpression(IqlExpression expression)
        {
            while (true)
            {
                if (expression == null)
                {
                    return null;
                }
                var finalExpression = expression as IqlFinalExpression<Expression>;
                if (finalExpression != null)
                {
                    return finalExpression.Value;
                }
                var aggregateExpression = expression as IqlAggregateExpression;
                if (aggregateExpression != null)
                {
                    throw new NotSupportedException($"{nameof(IqlAggregateExpression)} is not supported in DotNet");
//                    var aggregate = aggregateExpression;
//                    var str1 = "";
//                    aggregate.Expressions.ForEach(element =>
//                    {
//                        str1 += Parse(element
//#if TypeScript
//                        , evaluateContext
//#endif
//                        );
//                    });
//                    return str1;
                }
                var oldExpression = Expression;
                Expression = expression;
                var parser = Adapter.Registry.Resolve(Expression);
                if (parser == null)
                {
                    throw new Exception("No parser found for " + expression.GetType().Name);
                }
                var result = parser.ToQueryString(expression, this);
                // Reduce result
                var reducer = new IqlReducer(
#if TypeScript
                            evaluateContext
#endif
                );
                result = reducer.ReduceStaticContent(result);

                if (result != null)
                {
                    if (result is IqlFinalExpressionBase)
                    {
                        return (result as IqlFinalExpression<Expression>).Value;
                    }
                    expression = result;
                    continue;
                }
                Expression = oldExpression;
                return null;
            }
        }
    }
}