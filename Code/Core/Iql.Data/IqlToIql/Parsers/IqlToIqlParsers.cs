using System;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Queryable;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.Services;

namespace Iql.Data.IqlToIql.Parsers
{
    public class IqlToIqlLiteralParser : IqlToIqlActionParserBase<IqlLiteralExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlLiteralExpression action, IqlToIqlParserContext parser)
        {
            var value = action.Value;
            if (value is IqlExpression)
            {
                value = (await parser.ParseAsync(value as IqlExpression)).Expression;
            }

            while (true)
            {
                if (value is IqlFinalExpressionBase)
                {
                    value = (value as IqlFinalExpressionBase).ResolveValue();
                }
                else if (value is IqlLiteralExpression)
                {
                    value = (value as IqlLiteralExpression).Value;
                }
                else
                {
                    break;
                }
            }
            action.Value = value;
            return action;
        }
    }

    public class IqlToIqlSpecialValueParser : IqlToIqlActionParserBase<IqlSpecialValueExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlSpecialValueExpression action, IqlToIqlParserContext parser)
        {
            var currentUserService = parser.ServiceProvider.Resolve<IqlCurrentUserService>();
            switch (action.Kind)
            {
                case IqlExpressionKind.CurrentUserId:
                    object currentUserId = null;
                    if (currentUserService != null)
                    {
                        currentUserId = await currentUserService.ResolveCurrentUserIdAsync(parser.ServiceProvider);
                    }
                    return new IqlLiteralExpression(currentUserId);
                case IqlExpressionKind.CurrentUser:
                    object currentUser = null;
                    if (currentUserService != null)
                    {
                        currentUser = await currentUserService.ResolveCurrentUserAsync(parser.ServiceProvider);
                    }
                    return new IqlLiteralExpression(currentUser);
            }
            return action;
        }
    }

    public class IqlToIqlIntersectsParser : IqlToIqlActionParserBase<IqlIntersectsExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlIntersectsExpression action, IqlToIqlParserContext parser)
        {
            if (action.Polygon == null)
            {
                return null;
            }

            if (action.Polygon.Kind == IqlExpressionKind.Literal ||
                action.Polygon.Kind == IqlExpressionKind.Variable)
            {
                var literal = action.Polygon as IqlLiteralExpression;
                if (literal == null)
                {
                    return null;
                }

                var polygon = literal.Value as IqlPolygonExpression;
                if (polygon == null)
                {
                    return null;
                }

                if (polygon.OuterRing == null)
                {
                    return null;
                }

                if (polygon.OuterRing.Points == null || polygon.OuterRing.Points.Count < 3)
                {
                    return null;
                }
            }
            return action;
        }
    }

    public class IqlToIqlLambdaParser : IqlToIqlActionParserBase<IqlLambdaExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlLambdaExpression action, IqlToIqlParserContext parser)
        {
            action.Body = (await parser.ParseAsync(action.Body)).Expression;
            if (action.Body == null)
            {
                return null;
            }
            if (action.Parameters != null)
            {
                for (var i = 0; i < action.Parameters.Count; i++)
                {
                    action.Parameters[i] = (IqlRootReferenceExpression)(await parser.ParseAsync(action.Parameters[i])).Expression;
                    var iqlRootReferenceExpression = action.Parameters[i];
                    if (!string.IsNullOrWhiteSpace(iqlRootReferenceExpression.EntityTypeName))
                    {
                        parser.ResolveSpecialTypeMap(specialTypeMap =>
                        {
                            iqlRootReferenceExpression.EntityTypeName = specialTypeMap.EntityConfiguration.Type.Name;
                        });
                    }
                }
            }
            action.Parent = (IqlExpression)(await parser.ParseAsync(action.Parent)).Expression;
            return action;
        }
    }

    public class IqlToIqlPropertyParser : IqlToIqlActionParserBase<IqlPropertyExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlPropertyExpression action, IqlToIqlParserContext parser)
        {
            var property = parser.Adapter.EntityConfigurationContext.EntityType<TEntity>()
                .FindProperty(action.PropertyName);
            if (property != null && property.Kind.HasFlag(PropertyKind.Count))
            {
                action.PropertyName = property.Relationship.ThisEnd.Property.Name;
                return new IqlCountExpression(null, action, null);
            }
            parser.ResolveSpecialTypeMap(specialTypeMap =>
            {
                var mappedProperty = specialTypeMap.ResolvePropertyMap(action.PropertyName);
                if (mappedProperty != null)
                {
                    action.PropertyName = mappedProperty.PropertyName;
                    action.ReturnType = mappedProperty.TypeDefinition.ToIqlType();
                }
            });
            action.Parent = (IqlExpression)(await parser.ParseAsync(action.Parent)).Expression;
            return action;
        }
    }

    public class IqlToIqlAggregateParser : IqlToIqlActionParserBase<IqlAggregateExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlAggregateExpression action, IqlToIqlParserContext parser)
        {
            if (action.Expressions != null)
            {
                for (var i = 0; i < action.Expressions.Count; i++)
                {
                    action.Expressions[i] = (IqlExpression)(await parser.ParseAsync(action.Expressions[i])).Expression;
                }
            }
            action.Parent = (IqlExpression)(await parser.ParseAsync(action.Parent)).Expression;

            return action;
        }
    }


    public class IqlToIqlBinaryParser : IqlToIqlActionParserBase<IqlBinaryExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlBinaryExpression action, IqlToIqlParserContext parser)
        {
            var lr = new[] { action.Left, action.Right };
            var isValidEnumCheck = action.Kind == IqlExpressionKind.Has || action.Kind == IqlExpressionKind.IsEqualTo ||
                         action.Kind == IqlExpressionKind.IsNotEqualTo;
            var propertyExpression = lr.FirstOrDefault(_ => _ != null && _.Kind == IqlExpressionKind.Property) as IqlPropertyExpression;
            var enumLiteralExpression = lr.FirstOrDefault(_ => _ != null && _.Kind == IqlExpressionKind.EnumLiteral) as IqlEnumLiteralExpression;
            var literal = lr.FirstOrDefault(_ => _ != null && _.Kind == IqlExpressionKind.Literal) as IqlLiteralExpression;
            if (propertyExpression != null &&
                enumLiteralExpression != null &&
                isValidEnumCheck)
            {
                if (enumLiteralExpression.Value == null)
                {
                    return null;
                }
            }
            else if (propertyExpression != null &&
                     literal != null &&
                     isValidEnumCheck &&
                     (literal.ReturnType == IqlType.Integer || literal.InferredReturnType == IqlType.Integer))
            {
                var type = propertyExpression.ResolveType(parser.CurrentEntityType);
                if (type?.IsDefined(typeof(FlagsAttribute), true) == true)
                {
                    if (literal.Value == null)
                    {
                        return null;
                    }
                }
            }

            action.Parent = (IqlExpression)(await parser.ParseAsync(action.Parent)).Expression;

            if (literal != null &&
                (action.Kind == IqlExpressionKind.IsEqualTo && Equals(literal.Value, false)) ||
                (action.Kind == IqlExpressionKind.IsNotEqualTo && Equals(literal.Value, true)))
            {
                return (await parser.ReplaceAndParseAsync(new IqlNotExpression(lr.SingleOrDefault(l => l != literal) ?? literal))).Expression;
            }

            action.Left = (await parser.ParseAsync(action.Left)).Expression;
            action.Right = (await parser.ParseAsync(action.Right)).Expression;

            if (action.Left == null && action.Right != null)
            {
               return action.Right;
            }
            if (action.Left != null && action.Right == null)
            {
                return action.Left;
            }
            if (action.Left == null && action.Right == null)
            {
                return null;
            }

            lr = new[] { action.Left, action.Right };
            literal = lr.FirstOrDefault(_ => _ != null && _.Kind == IqlExpressionKind.Literal) as IqlLiteralExpression;

            //var isEqualTo = lr.FirstOrDefault(_ => _ != null && _.Kind == IqlExpressionKind.IsEqualTo);
            //var isNotEqualTo = lr.FirstOrDefault(_ => _ != null && _.Kind == IqlExpressionKind.IsNotEqualTo);

            var indexOf = lr.FirstOrDefault(_ => _ != null && _.Kind == IqlExpressionKind.StringIndexOf);

            if (indexOf != null && literal != null && Equals(literal.Value, -1))
            {
                var negationCount = 0;
                for (var i = parser.Ancestors.Count - 1; i >= 0; i--)
                {
                    if (parser.Ancestors[i] == action)
                    {
                        continue;
                    }

                    if (parser.Ancestors[i].Kind == IqlExpressionKind.Not)
                    {
                        negationCount++;
                        continue;
                    }

                    break;
                }

                if ((action.Kind == IqlExpressionKind.IsNotEqualTo && negationCount % 2 != 0) ||
                    (action.Kind == IqlExpressionKind.IsEqualTo && negationCount % 2 == 0))
                {
                    return IqlToIqlNotParser.AppendStringIsNullOrEmptyCheck(action, indexOf.Parent);
                }
            }

            return action;
        }
    }


    public class IqlToIqlNavigationParser : IqlToIqlActionParserBase<IqlNavigationExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlNavigationExpression action, IqlToIqlParserContext parser)
        {
            if (action.Expands != null)
            {
                for (var i = 0; i < action.Expands.Count; i++)
                {
                    action.Expands[i] = (IqlExpandExpression)(await parser.ParseAsync(action.Expands[i])).Expression;
                }
            }
            action.Filter = (IqlExpression)(await parser.ParseAsync(action.Filter)).Expression;
            action.WithKey = (IqlWithKeyExpression)(await parser.ParseAsync(action.WithKey)).Expression;
            action.Parent = (IqlExpression)(await parser.ParseAsync(action.Parent)).Expression;

            return action;
        }
    }


    public class IqlToIqlCollectitonQueryParser : IqlToIqlActionParserBase<IqlCollectitonQueryExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlCollectitonQueryExpression action, IqlToIqlParserContext parser)
        {
            if (action.OrderBys != null)
            {
                for (var i = 0; i < action.OrderBys.Count; i++)
                {
                    action.OrderBys[i] = (IqlOrderByExpression)(await parser.ParseAsync(action.OrderBys[i])).Expression;
                }
            }
            if (action.Expands != null)
            {
                for (var i = 0; i < action.Expands.Count; i++)
                {
                    action.Expands[i] = (IqlExpandExpression)(await parser.ParseAsync(action.Expands[i])).Expression;
                }
            }
            action.Filter = (IqlExpression)(await parser.ParseAsync(action.Filter)).Expression;
            action.WithKey = (IqlWithKeyExpression)(await parser.ParseAsync(action.WithKey)).Expression;
            action.Parent = (IqlExpression)(await parser.ParseAsync(action.Parent)).Expression;

            return action;
        }
    }


    public class IqlToIqlDataSetQueryParser : IqlToIqlActionParserBase<IqlDataSetQueryExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlDataSetQueryExpression action, IqlToIqlParserContext parser)
        {
            action.DataSet = (IqlDataSetReferenceExpression)(await parser.ParseAsync(action.DataSet)).Expression;
            var reference = (IqlDataSetReferenceExpression)action.DataSet;
            if (reference != null)
            {
                parser.ResolveSpecialTypeMap(specialTypeMap =>
                {
                    reference.Name = specialTypeMap.EntityConfiguration.SetName;
                });
            }
            if (action.OrderBys != null)
            {
                for (var i = 0; i < action.OrderBys.Count; i++)
                {
                    action.OrderBys[i] = (IqlOrderByExpression)(await parser.ParseAsync(action.OrderBys[i])).Expression;
                }
            }
            if (action.Expands != null)
            {
                for (var i = 0; i < action.Expands.Count; i++)
                {
                    action.Expands[i] = (IqlExpandExpression)(await parser.ParseAsync(action.Expands[i])).Expression;
                }
            }
            action.Filter = (IqlExpression)(await parser.ParseAsync(action.Filter)).Expression;
            action.WithKey = (IqlWithKeyExpression)(await parser.ParseAsync(action.WithKey)).Expression;
            action.Parent = (IqlExpression)(await parser.ParseAsync(action.Parent)).Expression;

            return action;
        }
    }


    public class IqlToIqlExpandParser : IqlToIqlActionParserBase<IqlExpandExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlExpandExpression action, IqlToIqlParserContext parser)
        {
            var property = (await parser.ParseAsync(action.NavigationProperty)).Expression;
            if (property is IqlPropertyExpression)
            {
                action.NavigationProperty = (IqlPropertyExpression)property;
            }
            if (action.Query != null)
            {
                var path = IqlPropertyPath.FromPropertyExpression(
                    parser.Adapter.EntityConfigurationContext.EntityType<TEntity>(),
                    action.NavigationProperty);
                action.Query = (IqlCollectitonQueryExpression)(await action.Query.ProcessAsync(parser.Adapter.EntityConfigurationContext.GetEntityByType(
                    path.Property.TypeDefinition.ElementType), parser));
            }

            if (property is IqlCountExpression)
            {
                action.Count = true;
            }
            action.Parent = (IqlExpression)(await parser.ParseAsync(action.Parent)).Expression;

            return action;
        }
    }


    public class IqlToIqlParser : IqlToIqlActionParserBase<IqlExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlExpression action, IqlToIqlParserContext parser)
        {
            action.Parent = (IqlExpression)(await parser.ParseAsync(action.Parent)).Expression;

            return action;
        }
    }


    public class IqlToIqlNotParser : IqlToIqlActionParserBase<IqlNotExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlNotExpression action, IqlToIqlParserContext parser)
        {
            action.Expression = (IqlExpression)(await parser.ParseAsync(action.Expression)).Expression;
            action.Parent = (IqlExpression)(await parser.ParseAsync(action.Parent)).Expression;

            if (action.Expression.Kind == IqlExpressionKind.StringIncludes)
            {
                return AppendStringIsNullOrEmptyCheck(action, action.Expression.Parent);
            }

            return action;
        }

        public static IqlExpression AppendStringIsNullOrEmptyCheck(IqlExpression action, IqlExpression parent)
        {
            return new[]
            {
                action,
                new IqlIsEqualToExpression(parent,
                    new IqlLiteralExpression(null, IqlType.String)),
                new IqlIsEqualToExpression(parent,
                    new IqlLiteralExpression("", IqlType.String))
            }.Or();
        }
    }


    public class IqlToIqlOrderByParser : IqlToIqlActionParserBase<IqlOrderByExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlOrderByExpression action, IqlToIqlParserContext parser)
        {
            action.OrderExpression = (IqlExpression)(await parser.ParseAsync(action.OrderExpression)).Expression;
            action.Parent = (IqlExpression)(await parser.ParseAsync(action.Parent)).Expression;

            return action;
        }
    }


    public class IqlToIqlParenthesisParser : IqlToIqlActionParserBase<IqlParenthesisExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlParenthesisExpression action, IqlToIqlParserContext parser)
        {
            action.Expression = (IqlExpression)(await parser.ParseAsync(action.Expression)).Expression;
            action.Parent = (IqlExpression)(await parser.ParseAsync(action.Parent)).Expression;

            return action.Expression == null ? null : action;
        }
    }


    public class IqlToIqlParentValueParser : IqlToIqlActionParserBase<IqlParentValueExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlParentValueExpression action, IqlToIqlParserContext parser)
        {
            action.Value = (IqlExpression)(await parser.ParseAsync(action.Value)).Expression;
            action.Parent = (IqlExpression)(await parser.ParseAsync(action.Parent)).Expression;

            if (action.Value == null && action.Kind == IqlExpressionKind.All)
            {
                return null;
            }

            return action;
        }
    }


    public class IqlToIqlStringSubStringParser : IqlToIqlActionParserBase<IqlStringSubStringExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlStringSubStringExpression action, IqlToIqlParserContext parser)
        {
            action.Take = (IqlReferenceExpression)(await parser.ParseAsync(action.Take)).Expression;
            action.Value = (IqlExpression)(await parser.ParseAsync(action.Value)).Expression;
            action.Parent = (IqlExpression)(await parser.ParseAsync(action.Parent)).Expression;

            return action;
        }
    }


    public class IqlToIqlWithKeyParser : IqlToIqlActionParserBase<IqlWithKeyExpression>
    {
        public override async Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(IqlWithKeyExpression action, IqlToIqlParserContext parser)
        {
            if (action.KeyEqualToExpressions != null)
            {
                for (var i = 0; i < action.KeyEqualToExpressions.Count; i++)
                {
                    action.KeyEqualToExpressions[i] = (IqlIsEqualToExpression)(await parser.ParseAsync(action.KeyEqualToExpressions[i])).Expression;
                }
            }
            action.Parent = (IqlExpression)(await parser.ParseAsync(action.Parent)).Expression;

            return action;
        }
    }
}