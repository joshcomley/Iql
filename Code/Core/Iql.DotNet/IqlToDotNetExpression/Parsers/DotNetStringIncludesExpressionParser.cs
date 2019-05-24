using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Serialization;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetUnaryExpressionParser : DotNetActionParserBase<IqlUnaryExpression>
    {
        public override IqlExpression ToQueryString(IqlUnaryExpression action,
            DotNetIqlParserContext parser)
        {
            switch (action.Kind)
            {
                case IqlExpressionKind.UnarySubtract:
                    var value = action.Value.ClaimsToBeIql()
                        ? parser.Parse((IqlExpression) action.Value)
                        : parser.Parse(new IqlLiteralExpression(action.Value));
                    var unaryExpression = Expression.Negate(value.Expression);
                    return new IqlFinalExpression<Expression>(unaryExpression);
            }
            throw new NotImplementedException();
        }
    }
    public class DotNetStringIndexOfExpressionParser : DotNetActionParserBase<IqlStringIndexOfExpression>
    {
        static DotNetStringIndexOfExpressionParser()
        {
            var methods = typeof(string).GetMethods();
            StringIndexOfMethod = methods.Single(m =>
            {
                if (m.Name != nameof(string.IndexOf))
                {
                    return false;

                }

                var parameters = m.GetParameters();
                if (parameters.Length != 1)
                {
                    return false;
                }

                if (parameters[0].ParameterType != typeof(string))
                {
                    return false;
                }

                return true;
            });
            StringToUpperMethod = methods.Single(m => m.Name == nameof(string.ToUpper) && m.GetParameters().Length == 0);
        }

        public static MethodInfo StringToUpperMethod { get; set; }
        public static MethodInfo StringIndexOfMethod { get; set; }

        public override IqlExpression ToQueryString(IqlStringIndexOfExpression action,
            DotNetIqlParserContext parser)
        {
            var parent = parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
            );
            var parentExpression =
                DotNetExpressionConverter.DisableNullPropagation
                    ? parent.Expression
                    : Expression.Coalesce(parent.Expression, Expression.Constant(""));
            var valueExpression = parser.Parse(action.Value
#if TypeScript
                        , null
#endif
            ).Expression;
            valueExpression =
                DotNetExpressionConverter.DisableNullPropagation
                ? valueExpression
                : Expression.Coalesce(valueExpression, Expression.Constant(""));
            MethodCallExpression methodCallExpression;
            if (action.Value != null)
            {
                if (DotNetExpressionConverter.DisableCaseSensitivityHandling)
                {
                    methodCallExpression = Expression.Call(
                        parentExpression,
                        StringIndexOfMethod,
                        valueExpression
                    );
                }
                else
                {
                    methodCallExpression = Expression.Call(
                        Expression.Call(parentExpression, StringToUpperMethod),
                        StringIndexOfMethod,
                        Expression.Call(valueExpression,
                            StringToUpperMethod)
                    );
                }
            }
            else
            {
                return null;
            }
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    methodCallExpression
                );
            return expression;
        }
    }
    public class DotNetStringIncludesExpressionParser : DotNetActionParserBase<IqlStringIncludesExpression>
    {
        static DotNetStringIncludesExpressionParser()
        {
            var methods = typeof(string).GetMethods();
            StringContainsMethod = methods.Single(m =>
            {
                if (m.Name != nameof(string.Contains))
                {
                    return false;

                }

                var parameters = m.GetParameters();
                if (parameters.Length != 1)
                {
                    return false;
                }

                if (parameters[0].ParameterType != typeof(string))
                {
                    return false;
                }

                return true;
            });
            StringToUpperMethod = methods.Single(m => m.Name == nameof(string.ToUpper) && m.GetParameters().Length == 0);
        }

        public static MethodInfo StringToUpperMethod { get; set; }
        public static MethodInfo StringContainsMethod { get; set; }

        public override IqlExpression ToQueryString(IqlStringIncludesExpression action,
            DotNetIqlParserContext parser)
        {
            var parent = parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
            );
            var parentExpression =
                DotNetExpressionConverter.DisableNullPropagation
                    ? parent.Expression
                    : Expression.Coalesce(parent.Expression, Expression.Constant(""));
            var valueExpression = parser.Parse(action.Value
#if TypeScript
                        , null
#endif
            ).Expression;
            valueExpression =
                DotNetExpressionConverter.DisableNullPropagation
                ? valueExpression
                : Expression.Coalesce(valueExpression, Expression.Constant(""));
            MethodCallExpression methodCallExpression;
            if (action.Value != null)
            {
                if (DotNetExpressionConverter.DisableCaseSensitivityHandling)
                {
                    methodCallExpression = Expression.Call(
                        parentExpression,
                        StringContainsMethod,
                        valueExpression
                    );
                }
                else
                {
                    methodCallExpression = Expression.Call(
                        Expression.Call(parentExpression, StringToUpperMethod),
                        StringContainsMethod,
                        Expression.Call(valueExpression,
                            StringToUpperMethod)
                    );
                }
            }
            else
            {
                return null;
            }
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    methodCallExpression
                );
            return expression;
        }
    }
}