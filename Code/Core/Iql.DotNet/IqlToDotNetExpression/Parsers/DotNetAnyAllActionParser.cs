﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.DotNet.Extensions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetAnyAllActionParser : DotNetActionParserBase<IqlAnyAllExpression>
    {
        public override IqlExpression ToQueryString(IqlAnyAllExpression action, DotNetIqlParserContext parser)
        {
            MethodInfo method = null;
            switch (action.Kind)
            {
                case IqlExpressionKind.Any:
                    method = typeof(Enumerable).GetMethods().Single(m =>
                        m.Name == nameof(Enumerable.Any) && m.GetParameters().Length == 2);
                    break;
                case IqlExpressionKind.All:
                    method = typeof(Enumerable).GetMethods().Single(m =>
                        m.Name == nameof(Enumerable.All) && m.GetParameters().Length == 2);
                    break;
            }

            var accessors = ResolveAccessors(action.Parent as IqlPropertyExpression);
            var entityType = parser.CurrentEntityType;
            if (entityType == null)
            {
                return False();
            }
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
            if (parentExpression.Expression == null)
            {
                return False();
            }

            if (parentExpression.Expression is ConstantExpression constantExpression &&
                constantExpression.Value == null)
            {
                return False();
            }
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
            var lambda = predicate.Expression;
            method = method.MakeGenericMethod(entityType);
            var methodCallExpression =
                Expression.Call(
                    null,
                    method,
                    parentExpression.Expression,
                    lambda);
            var expression =
                new IqlFinalExpression<Expression>(
                    methodCallExpression
                );
            return expression;
        }

        private IqlExpression False()
        {
            return new IqlFinalExpression<Expression>(Expression.Constant(false));
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