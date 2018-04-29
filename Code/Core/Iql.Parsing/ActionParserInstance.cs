using System;
using System.Collections.Generic;
using Iql.Parsing.Reduction;
using Iql.Queryable.Expressions.Conversion;

namespace Iql.Parsing
{
    public abstract class ActionParserInstance<TIqlData, TQueryAdapter, TOutput, TParserOutput, TConverter> : IActionParserInstance
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData>
        where TParserOutput : IParserOutput
        where TConverter : IExpressionConverter
    {
        protected ActionParserInstance(TQueryAdapter adapter, Type rootEntityType, TConverter converter)
        {
            Adapter = adapter;
            RootEntityType = rootEntityType;
            Converter = converter;
            Data = Adapter.NewData();
        }

        public IqlExpression Expression { get; set; }
        public TIqlData Data { get; set; }
        public bool IsFilter { get; set; } = false;

        public TQueryAdapter Adapter { get; set; }
        public Type RootEntityType { get; }
        public TConverter Converter { get; }
        public Dictionary<string, string> _rootEntityNames { get; } = new Dictionary<string, string>();
        private string _rootEntityName = null;
        public string GetRootEntityName(IqlRootReferenceExpression rootReferenceExpression)
        {
            if (rootReferenceExpression == null)
            {
                return _rootEntityName;
            }
            var name = rootReferenceExpression.VariableName;
            return GetRootEntityParameterName(name);
        }

        public string GetRootEntityParameterName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "entity";
            }

            if (!_rootEntityNames.ContainsKey(name))
            {
                var index = _rootEntityNames.Count == 0 ? "" : (_rootEntityNames.Count + 1).ToString();
                var mappedName = $"entity{index}";
                _rootEntityNames.Add(name, mappedName);
            }

            if (_rootEntityName == null)
            {
                _rootEntityName = _rootEntityNames[name];
            }

            return _rootEntityNames[name];
        }

        public abstract TParserOutput Parse(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        );

        public virtual string ParseAsString(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
            )
        {
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
                        ).ToCodeString();
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