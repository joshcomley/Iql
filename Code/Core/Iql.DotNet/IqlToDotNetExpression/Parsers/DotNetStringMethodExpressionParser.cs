using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public abstract class DotNetStringMethodExpressionParser<TExpression> : DotNetActionParserBase<TExpression>
        where TExpression : IqlParentValueExpression
    {
        protected abstract MethodInfo StringMethod { get; }
        public override IqlExpression ToQueryString(TExpression action,
            DotNetIqlParserContext parser)
        {
            var parent = parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
            );
            var left = EnsureString(parent.Expression);
            var parentExpression =
                DotNetExpressionConverter.DisableNullPropagation
                    ? left
                    : Expression.Coalesce(left, Expression.Constant(""));
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
                        StringMethod,
                        valueExpression
                    );
                }
                else
                {
                    methodCallExpression = Expression.Call(
                        Expression.Call(parentExpression, StringMethods.StringToUpperMethod),
                        StringMethod,
                        Expression.Call(valueExpression,
                            StringMethods.StringToUpperMethod)
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

        private Expression EnsureString(Expression expression)
        {
            if (expression == null)
            {
                return Expression.Constant(null, typeof(string));
            }
            if (expression.NodeType == ExpressionType.Constant && expression.Type != typeof(string))
            {
                return Expression.Constant(((ConstantExpression)expression).Value, typeof(string));
            }
            return expression;
        }
    }
}