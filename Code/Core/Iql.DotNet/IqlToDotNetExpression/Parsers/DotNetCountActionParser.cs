using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.DotNet.Extensions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetCountActionParser : DotNetActionParserBase<IqlCountExpression>
    {
        public override IqlExpression ToQueryString(IqlCountExpression action, DotNetIqlParserInstance parser)
        {
            var method = typeof(Enumerable).GetMethods().Single(m =>
                m.Name == nameof(Enumerable.LongCount) && m.GetParameters().Length == 2);
            var accessors = ResolveAccessors(action.Parent as IqlPropertyExpression);
            var entityType = parser.RootEntityType;
            foreach (var accessor in accessors)
            {
                entityType = entityType.GetProperty(accessor).PropertyType;
            }
            entityType.TryGetBaseType(typeof(IEnumerable<>), type =>
            {
                entityType = type.Type.GenericTypeArguments[0];
            });

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
            );
            method = method.MakeGenericMethod(entityType);
            var methodCallExpression =
                Expression.Call(
                    null,
                    method,
                    parentExpression.Expression,
                    predicate.Expression);
            var expression =
                new IqlFinalExpression<Expression>(
                    methodCallExpression
                );
            return expression;
        }

        private static string[] ResolveAccessors(IqlPropertyExpression propertyExpression)
        {
            var accessors = new List<string>();
            var expression = propertyExpression;
            accessors.Add(expression.PropertyName);
            while (expression.Parent is IqlPropertyExpression)
            {
                expression = expression.Parent as IqlPropertyExpression;
                accessors.Add(expression.PropertyName);
            }
            accessors.Reverse();
            return accessors.ToArray();
        }
    }
}