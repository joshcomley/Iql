using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.DotNet.Parsers;
using Iql.DotNet.Serialization;
using Iql.Extensions;
using Iql.Parsing;
using Iql.Parsing.Extensions;
using Iql.Parsing.Reduction;
using Iql.Parsing.Reduction.Reducers;

namespace Iql.DotNet
{
    public class ExpressionToIqlExpressionParser<T>
    {
        public ExpressionToIqlExpressionParser()
        {
            Parsers.Add(() => new BinaryExpressionParser<T>());
            Parsers.Add(() => new ConstantExpressionParser<T>());
            Parsers.Add(() => new LambdaExpressionParser<T>());
            Parsers.Add(() => new MemberAccessExpressionParser<T>());
            Parsers.Add(() => new MethodCallExpressionParser<T>());
            Parsers.Add(() => new ParameterExpressionParser<T>());
        }

        public IqlExpression ResolvedExpression { get; set; }

        public List<Func<IExpressionParser>> Parsers { get; set; }
            = new List<Func<IExpressionParser>>();

        public static IqlExpression Parse<TResult>(Expression<Func<T, TResult>> exp)
        {
            return new ExpressionToIqlExpressionParser<T>()
                .ToIqlExpression(exp);
        }

        public static IqlExpression Parse(LambdaExpression exp, EvaluateContext evaluateContext)
        {
            return new ExpressionToIqlExpressionParser<T>()
                .ToIqlExpression(exp);
        }

        public IqlExpression ToIqlExpression(LambdaExpression exp)
        {
            var root = exp.Parameters[0];
            var parsers = Parsers.Select(p => p()).ToList();
            var iql = GetIqlExpression(exp, parsers, new ExpressionParserContext(
                root.Type,
                root.Name,
                (node, context) => GetIqlExpression(node, parsers, context)));
            var reducer = new IqlReducer(
                registry: new IqlReducerRegistry());
            return reducer.ReduceStaticContent(iql);
        }

        private IqlExpression GetIqlExpression(
            Expression exp,
            List<IExpressionParser> parsers,
            ExpressionParserContext context)
        {
            if (exp == null)
            {
                return null;
            }
            if (!context.ContainsRoot(exp))
            {
                var value = exp.GetValue();
                var type = exp.Type;
                if (value != null)
                {
                    type = value.GetType();
                }
                return new IqlLiteralExpression(value, type.ToIqlType());
            }
            var parser = parsers.FirstOrDefault(p => p.CanHandle(exp));
            if (parser == null)
            {
                throw Error.NotSupported("SRResources.UnsupportedExpressionNodeTypeWithName: {0}", exp.NodeType);
            }
            return parser.Parse(exp, context);
        }
    }
}