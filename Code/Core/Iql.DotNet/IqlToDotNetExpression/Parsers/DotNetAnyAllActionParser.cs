using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.DotNet.Extensions;
using Iql.Extensions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetAnyAllActionParser : DotNetActionParserBase<IqlAnyAllExpression>
    {
        public override IqlExpression ToQueryString(IqlAnyAllExpression action, DotNetIqlParserInstance parser)
        {
            MethodInfo method = null;
            switch (action.Type)
            {
                case IqlExpressionType.Any:
                    method = typeof(Enumerable).GetMethods().Single(m =>
                        m.Name == nameof(Enumerable.Any) && m.GetParameters().Length == 2);
                    break;
                case IqlExpressionType.All:
                    method = typeof(Enumerable).GetMethods().Single(m =>
                        m.Name == nameof(Enumerable.All) && m.GetParameters().Length == 2);
                    break;
            }
            var parentExpression = parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
            );
            //var expressionType = parentExpression.Expression.Type;
            //var elementType = expressionType;
            //expressionType.TryGetBaseType(typeof(IEnumerable<>), type =>
            //{
            //    elementType = type.Type.GenericTypeArguments[0];
            //});
            var predicate = parser.Parse(action.Value
#if TypeScript
                        , null
#endif
            ).Expression;
            method = method.MakeGenericMethod(predicate.Type.GetGenericArguments()[0]);
            var methodCallExpression =
                Expression.Call(
                    null,
                    method,
                    parentExpression.Expression,
                    predicate);
            var expression =
                new IqlFinalExpression<Expression>(
                    methodCallExpression
                );
            return expression;
        }
    }
}