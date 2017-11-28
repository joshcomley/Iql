using System;
using Iql.Parsing.Reduction;

namespace Iql.Parsing
{
    public abstract class ActionParserInstance<TIqlData, TQueryAdapter, TOutput, TParserOutput> : IActionParserInstance
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData>
        where TParserOutput : IParserOutput
    {
        protected ActionParserInstance(TQueryAdapter adapter, Type rootEntityType)
        {
            Adapter = adapter;
            RootEntityType = rootEntityType;
            Data = Adapter.NewData();
        }

        public IqlExpression Expression { get; set; }
        public TIqlData Data { get; set; }
        public bool IsFilter { get; set; } = false;

        public TQueryAdapter Adapter { get; set; }
        public Type RootEntityType { get; }

        public abstract TParserOutput Parse(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        );

        public virtual string ParseAsString(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
            ){
            while (true)
            {
                if (expression == null)
                {
                    return "";
                }
                var finalExpression = expression as IqlFinalExpression<string>;
                if (finalExpression != null)
                {
                    return finalExpression.Value;
                }
                var aggregateExpression = expression as IqlAggregateExpression;
                if (aggregateExpression != null)
                {
                    var aggregate = aggregateExpression;
                    var str1 = "";
                    aggregate.Expressions.ForEach(element =>
                    {
                        str1 += Parse(element
#if TypeScript
                        , evaluateContext
#endif
                        );
                    });
                    return str1;
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
                        return (result as IqlFinalExpression<string>).Value;
                    }
                    expression = result;
                    continue;
                }
                Expression = oldExpression;
                return null;
            }
        }

    object IActionParserInstance.Parse(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
        {
            return Parse(expression
#if TypeScript
            , evaluateContext
#endif
                );
        }
        //     }
        //         return "";

        //    public void parse<TAction extends IqlExpression>(action: TAction): string {
    }
}