using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Parsing;
using Iql.Parsing.Reduction;

namespace Iql.DotNet.IqlToDotNetExpression
{
    public class DotNetIqlParserInstance : ActionParserInstance<DotNetIqlData, DotNetIqlExpressionAdapter, Expression, DotNetOutput>
    {
        public DotNetIqlParserInstance(DotNetIqlExpressionAdapter adapter, Type rootEntityType) : base(adapter, rootEntityType)
        {
            RootEntities.Add(NewRootParameter(rootEntityType));
        }

        private ParameterExpression NewRootParameter(Type rootEntityType)
        {
            return System.Linq.Expressions.Expression.Parameter(rootEntityType, $"entity{(RootEntities.Count == 0 ? "" : RootEntities.Count.ToString())}");
        }

        public List<ParameterExpression> RootEntities { get; } = new List<ParameterExpression>();
        public ParameterExpression RootEntity => RootEntities.Last();
        public bool ConvertToLambda { get; set; } = true;

        public DotNetOutput ParseLambda(IqlExpression expression, Type rootEntityType
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
        {
            RootEntities.Add(NewRootParameter(rootEntityType));
            var result = Parse(expression
#if TypeScript
                , evaluateContext
#endif
            );
            RootEntities.RemoveAt(RootEntities.Count - 1);
            return result;
        }

        public override DotNetOutput Parse(IqlExpression expression
#if TypeScript
                , EvaluateContext evaluateContext
#endif
        )
        {
            return new DotNetOutput(RootEntity, ParseAsExpression(expression
#if TypeScript
                , evaluateContext
#endif
            ));
        }

        private Expression ParseAsExpression(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
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