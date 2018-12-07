using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Parsing.Reduction;
using Iql.Parsing.Types;

namespace Iql.Parsing
{
    public abstract class AsyncActionParserContext<TIqlData, TQueryAdapter, TOutput, TParserOutput, TConverter> :
        ActionParserContextBase<
            IqlAsyncParserRegistry,
            TIqlData,
            TQueryAdapter,
            TOutput,
            TParserOutput,
            AsyncActionParserContext<TIqlData, TQueryAdapter, TOutput, TParserOutput, TConverter>,
            TConverter,
            IAsyncActionParserBase>,
        IAsyncActionParserContext
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData, IqlAsyncParserRegistry, IAsyncActionParserBase>
        where TParserOutput : IParserOutput
        where TConverter : IExpressionConverter
    {
        protected AsyncActionParserContext(TQueryAdapter adapter, Type currentEntityType, TConverter converter, ITypeResolver typeResolver) : base(adapter, currentEntityType, converter, typeResolver)
        {
        }


        public Task<TParserOutput> ReplaceAndParse(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            Ancestors[Ancestors.Count - 1] = expression;
            return ParseInternal(expression,
                false
#if TypeScript
, evaluateContext
#endif
            );
        }

        public async Task<TParserOutput[]> ParseAll(IEnumerable<IqlExpression> expressions)
        {
            if (expressions == null)
            {
                return new TParserOutput[] { };
            }
            var result = new List<TParserOutput>();
            var iqlExpressions = expressions.ToArray();
            for (var i = 0; i < iqlExpressions.Length; i++)
            {
                var expression = iqlExpressions[i];
                result.Add(await Parse(expression));
            }

            return result.ToArray();
        }

        public abstract Task<TParserOutput> ParseExpression(IqlExpression expression
#if TypeScript
                    , EvaluateContext evaluateContext = null
#endif
                );



        public Task<TParserOutput> Parse(IqlExpression expression
#if TypeScript
                    , EvaluateContext evaluateContext = null
#endif
                )
        {
            // Here: figure out the path from the root entity
            return ParseInternal(expression, true
#if TypeScript
        , evaluateContext
#endif
                    );
        }

        protected virtual async Task<TParserOutput> ParseInternal(IqlExpression expression,
            bool appendToAncestors
#if TypeScript
                    , EvaluateContext evaluateContext = null
#endif
                )
        {
            // Here: figure out the path from the root entity
            if (appendToAncestors)
            {
                Ancestors.Add(expression);
            }
            IncrementPath(expression);
            var index = appendToAncestors ? Ancestors.Count - 1 : -1;
            var result = await ParseExpression(expression);
            if (expression != null)
            {
                if (!OutputMap.ContainsKey(expression))
                {
                    OutputMap.Add(expression, new List<TParserOutput>());
                }
                OutputMap[expression].Add(result);
            }

            if (index != -1)
            {
                Ancestors.RemoveAt(index);
            }
            DecrementPath(expression);
            return result;
        }

        public virtual async Task<string> ParseAsString(IqlExpression expression
#if TypeScript
                    , EvaluateContext evaluateContext = null
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
                    foreach (var element in aggregate.Expressions)
                    {
                        str1 += (await Parse(element
#if TypeScript
                                , evaluateContext
#endif
                        )).ToCodeString();
                    }
                    return str1;
                }
                var oldExpression = Expression;
                Expression = expression;
                var parser = Adapter.Registry.Resolve(IqlExpression.ResolveExpressionType(Expression));
                if (parser == null)
                {
                    throw new Exception("No parser found for " + expression.GetType().Name);
                }

                //IqlExpression result = null;
                var result = await parser.ToQueryStringAsync(expression, this);
                // Reduce result
                var reducer = new IqlReducer(
#if TypeScript
                                    evaluateContext
#endif
                        );
                reducer.Ancestors = Ancestors.ToList();
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

        public async Task<object> ParseActionAsync(IqlExpression expression)
        {
            return await Parse(expression);
        }
    }
}