using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetStringStringIncludesExpressionParser : DotNetActionParserBase<IqlStringIncludesExpression>
    {
        static DotNetStringStringIncludesExpressionParser()
        {
            var methods = typeof(string).GetMethods();
            //var stringContainsMethods = methods.Where(m =>
            //{
            //    if (m.Name != nameof(string.Contains))
            //    {
            //        return false;

            //    }

            //    var parameters = m.GetParameters();
            //    if (parameters.Length != 1)
            //    {
            //        return false;
            //    }

            //    if (parameters[0].ParameterType != typeof(string))
            //    {
            //        return false;
            //    }

            //    return true;
            //}).ToArray();
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
            var parentExpression = Expression.Coalesce(parent.Expression, Expression.Constant(""));
            var valueExpression = parser.Parse(action.Value
#if TypeScript
                        , null
#endif
            ).Expression;
            valueExpression = Expression.Coalesce(valueExpression, Expression.Constant(""));
            MethodCallExpression methodCallExpression;
            if (action.Value != null)
            {
                methodCallExpression = Expression.Call(
                    Expression.Call(parentExpression, StringToUpperMethod),
                    StringContainsMethod,
                    Expression.Call(valueExpression,
                        StringToUpperMethod)
                );
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