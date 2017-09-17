using System;
using Iql.Parsing.Reduction;

namespace Iql.Parsing
{
    public class ActionParserInstance<TIqlData, TQueryAdapter> : IActionParserInstance
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData>
    {
        public ActionParserInstance(TQueryAdapter adapter)
        {
            Adapter = adapter;
            Data = Adapter.NewData();
        }

        public IqlExpression Expression { get; set; }
        public TIqlData Data { get; set; }
        public bool IsFilter { get; set; } = false;

        public TQueryAdapter Adapter { get; set; }

        public string Parse(IqlExpression expression, EvaluateContext evaluateContext)
        {
            while (true)
            {
                if (expression == null)
                {
                    return "";
                }
                var finalExpression = expression as IqlFinalExpression;
                if (finalExpression != null)
                {
                    return finalExpression.Value;
                }
                var aggregateExpression = expression as IqlAggregateExpression;
                if (aggregateExpression != null)
                {
                    var aggregate = aggregateExpression;
                    var str1 = "";
                    aggregate.Expressions.ForEach(element => { str1 += Parse(element, evaluateContext); });
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
                var reducer = new IqlReducer(evaluateContext);
                result = reducer.ReduceStaticContent(result);

                if (result != null)
                {
                    if (result is IqlFinalExpression)
                    {
                        return (result as IqlFinalExpression).Value;
                    }
                    expression = result;
                    continue;
                }
                Expression = oldExpression;
                return null;
            }
        }
        //     }
        //         return "";

        //    public void parse<TAction extends IqlExpression>(action: TAction): string {
    }
}