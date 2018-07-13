using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.DotNet.DotNetExpressionToIql.Parsers;
using Iql.Extensions;
#if TypeScript
using Iql.Parsing;
#endif
using Iql.Parsing.Extensions;
using Iql.Parsing.Reduction;
using Iql.Parsing.Reduction.Reducers;

namespace Iql.DotNet.DotNetExpressionToIql
{
    public class DotNetExpressionToIqlExpressionParser<T>
    {
        public DotNetExpressionToIqlExpressionParser()
        {
            Parsers.Add(() => new BinaryDotNetExpressionParser<T>());
            Parsers.Add(() => new ConstantDotNetExpressionParser<T>());
            Parsers.Add(() => new ConditionalDotNetExpressionParser<T>());
            Parsers.Add(() => new LambdaDotNetExpressionParser<T>());
            Parsers.Add(() => new MemberAccessDotNetExpressionParser<T>());
            Parsers.Add(() => new MethodCallDotNetExpressionParser<T>());
            Parsers.Add(() => new UnaryDotNetExpressionParser<T>());
            Parsers.Add(() => new ParameterDotNetExpressionParser<T>());
        }

        public List<Func<IDotNetExpressionParser>> Parsers { get; set; }
            = new List<Func<IDotNetExpressionParser>>();

        public static IqlExpression Parse<TResult>(Expression<Func<T, TResult>> exp)
        {
            return new DotNetExpressionToIqlExpressionParser<T>()
                .ToIqlExpression(exp);
        }

        public static IqlExpression Parse(LambdaExpression exp)
        {
            return new DotNetExpressionToIqlExpressionParser<T>()
                .ToIqlExpression(exp);
        }

        public IqlExpression ToIqlExpression(LambdaExpression exp)
        {
            var root = exp.Parameters[0];
            var parsers = Parsers.Select(p => p()).ToList();
            var iql = GetIqlExpression(exp, parsers, new DotNetExpressionParserContext(
                root.Type,
                root.Name,
                (node, context) => GetIqlExpression(node, parsers, context)));
            var reducer = new IqlReducer();
            var parsed = reducer.ReduceStaticContent(iql);
            return parsed;
        }

        private IqlExpression GetIqlExpression(
            Expression exp,
            List<IDotNetExpressionParser> parsers,
            DotNetExpressionParserContext context)
        {
            if (exp == null)
            {
                return null;
            }
            if (exp.NodeType == ExpressionType.Lambda)
            {
                var parameterExpression = (exp as LambdaExpression).Parameters[0];
                context.RootVariableNames.Add(parameterExpression.Name);
                context.RootVariableTypes.Add(parameterExpression.Type);
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
            var result = parser.Parse(exp, context);
            if (exp.NodeType == ExpressionType.Lambda)
            {
                context.RootVariableNames.RemoveAt(context.RootVariableNames.Count - 1);
                context.RootVariableTypes.RemoveAt(context.RootVariableTypes.Count - 1);
            }

            return result;
        }
    }
}