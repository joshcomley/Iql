using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Conversion;
using Iql.Data.Configuration;
using Iql.Parsing.Reduction;
using Iql.Parsing.Types;

namespace Iql.Parsing
{
    public abstract class ActionParserInstance<TIqlData, TQueryAdapter, TOutput, TParserOutput, TConverter> : IActionParserInstance
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData>
        where TParserOutput : IParserOutput
        where TConverter : IExpressionConverter
    {
        public bool Nested => false;

        protected ActionParserInstance(TQueryAdapter adapter, Type rootEntityType, TConverter converter, ITypeResolver typeResolver)
        {
            Adapter = adapter;
            RootEntityType = rootEntityType;
            Converter = converter;
            TypeResolver = typeResolver;
            Data = Adapter.NewData();
        }

        public IqlExpression Expression { get; set; }
        public TIqlData Data { get; set; }
        public TQueryAdapter Adapter { get; set; }
        public Type RootEntityType { get; }
        public TConverter Converter { get; }
        public ITypeResolver TypeResolver { get; }
        private readonly Dictionary<string, string> _rootEntityNames = new Dictionary<string, string>();
        private string _rootEntityName;

        public string GetRootEntityName(IqlRootReferenceExpression rootReferenceExpression)
        {
            if (rootReferenceExpression == null)
            {
                return _rootEntityName;
            }
            var name = rootReferenceExpression.VariableName;
            return GetRootEntityParameterName(name);
        }

        public string RootEntityParameterName()
        {
            return _rootEntityName;
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

        public TParserOutput[] ParseAll(IEnumerable<IqlExpression> expressions)
        {
            if (expressions == null)
            {
                return new TParserOutput[] { };
            }
            var result = new List<TParserOutput>();
            foreach (var expression in expressions)
            {
                result.Add(Parse(expression));
            }
            return result.ToArray();
        }

        public List<IqlExpression> Ancestors { get; } = new List<IqlExpression>();

        public T[] GetAncestors<T>()
        where T : IqlExpression
        {
            return Ancestors
                .Where(a => a is T).Select(a => a as T)
                .ToArray();
        }

        public T GetNearestAncestor<T>()
            where T : IqlExpression
        {
            for (var i = Ancestors.Count - 1; i >= 0; i--)
            {
                if (Ancestors[i] is T)
                {
                    return Ancestors[i] as T;
                }
            }

            return null;
        }

        public Type ResolveParameterType(string name)
        {
            var typeName = ResolveParameterTypeName(name);
            if (string.IsNullOrWhiteSpace(typeName))
            {
                return RootEntityType;
            }

            return ResolveTypeFromTypeName(typeName);
        }

        protected Type ResolveTypeFromTypeName(string typeName)
        {
            return TypeResolver.ResolveTypeFromTypeName(typeName);
        }

        public string ResolveParameterTypeName(string name)
        {
            var lambda = ResolveLambdaExpressionForParameter(name);
            return lambda.ParameterExpression?.EntityTypeName;
        }

        public LambdaAndParameter ResolveLambdaExpressionForParameter(string name)
        {
            //if (!string.IsNullOrWhiteSpace(parameter.EntityTypeName))
            //{
            //    return parameter.EntityTypeName;
            //}

            var lambdaExpressions = GetAncestors<IqlParameteredExpression>();

            for (var i = lambdaExpressions.Length - 1; i >= 0; i--)
            {
                var iqlParameteredExpression = lambdaExpressions[i];
                if (iqlParameteredExpression.Parameters != null)
                {
                    var lambdaParameter = iqlParameteredExpression.Parameters
                        .SingleOrDefault(p => (p.VariableName ?? "") == name);
                    if (lambdaParameter != null)
                    {
                        return new LambdaAndParameter(iqlParameteredExpression, lambdaParameter);
                        // && !string.IsNullOrWhiteSpace(lambdaParameter.EntityTypeName)
                        //return lambdaParameter.EntityTypeName;
                    }
                }
            }

            return null;
        }

        public Dictionary<IqlExpression, List<TParserOutput>> OutputMap { get; } = new Dictionary<IqlExpression, List<TParserOutput>>();
        public TParserOutput Parse(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            // Here: figure out the path from the root entity
            Ancestors.Add(expression);
            IncrementPath(expression);
            var index = Ancestors.Count - 1;
            var result = ParseExpression(expression);
            if (expression != null)
            {
                if (!OutputMap.ContainsKey(expression))
                {
                    OutputMap.Add(expression, new List<TParserOutput>());
                }
                OutputMap[expression].Add(result);
            }
            Ancestors.RemoveAt(index);
            DecrementPath(expression);
            return result;
        }

        private void DecrementPath(IqlExpression expression)
        {
            //EntityConfigurationBuilder.FindConfigurationBuilderForEntityType()
            switch (expression?.Kind)
            {
                case IqlExpressionKind.Count:
                    break;
                case IqlExpressionKind.Any:
                    break;
                case IqlExpressionKind.All:
                    break;
                case IqlExpressionKind.Lambda:
                    break;
                case IqlExpressionKind.DataSetQuery:
                    break;
            }
        }

        protected string Path { get; set; }
        private void IncrementPath(IqlExpression expression)
        {
            switch (expression?.Kind)
            {
                case IqlExpressionKind.Count:
                    break;
                case IqlExpressionKind.Any:
                    break;
                case IqlExpressionKind.All:
                    break;
                case IqlExpressionKind.Lambda:
                    break;
                case IqlExpressionKind.DataSetQuery:
                    
                    break;
            }
        }

        public IqlExpression Parent()
        {
            if (Ancestors.Count < 2)
            {
                return null;
            }
            return Ancestors[Ancestors.Count - 2];
        }

        public abstract TParserOutput ParseExpression(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        public virtual string ParseAsString(IqlExpression expression
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
                var parser = Adapter.Registry.Resolve(IqlExpression.ResolveExpressionType(Expression));
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

    public class LambdaAndParameter
    {
        public IqlParameteredExpression LambdaExpression { get; set; }
        public IqlRootReferenceExpression ParameterExpression { get; set; }
        public LambdaAndParameter(IqlParameteredExpression lambdaExpression, IqlRootReferenceExpression parameterExpression)
        {
            LambdaExpression = lambdaExpression;
            ParameterExpression = parameterExpression;
        }
    }
}