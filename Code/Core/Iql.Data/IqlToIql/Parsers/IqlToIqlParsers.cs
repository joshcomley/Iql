using System;
using System.Linq;
using Iql.Data.Extensions;
using Iql.Data.Queryable;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.SpecialTypes;

namespace Iql.Data.IqlToIql.Parsers
{

    public class IqlToIqlIntersectsParser : IqlToIqlActionParserBase<IqlIntersectsExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlIntersectsExpression action, IqlToIqlParserInstance parser)
        {
            if (action.Polygon == null)
            {
                return null;
            }

            if (action.Polygon.Kind == IqlExpressionKind.Literal)
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
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlLambdaExpression action, IqlToIqlParserInstance parser)
        {
            action.Body = parser.Parse(action.Body).Expression;
            if (action.Body == null)
            {
                return null;
            }
            if (action.Parameters != null)
            {
                for (var i = 0; i < action.Parameters.Count; i++)
                {
                    action.Parameters[i] = (IqlRootReferenceExpression)parser.Parse(action.Parameters[i]).Expression;
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
            action.Parent = (IqlExpression)parser.Parse(action.Parent).Expression;
            return action;
        }
    }

    public class IqlToIqlPropertyParser : IqlToIqlActionParserBase<IqlPropertyExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlPropertyExpression action, IqlToIqlParserInstance parser)
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
            action.Parent = (IqlExpression)parser.Parse(action.Parent).Expression;
            return action;
        }
    }

    public class IqlToIqlAggregateParser : IqlToIqlActionParserBase<IqlAggregateExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlAggregateExpression action, IqlToIqlParserInstance parser)
        {
            if (action.Expressions != null)
            {
                for (var i = 0; i < action.Expressions.Count; i++)
                {
                    action.Expressions[i] = (IqlExpression)parser.Parse(action.Expressions[i]).Expression;
                }
            }
            action.Parent = (IqlExpression)parser.Parse(action.Parent).Expression;

            return action;
        }
    }


    public class IqlToIqlBinaryParser : IqlToIqlActionParserBase<IqlBinaryExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlBinaryExpression action, IqlToIqlParserInstance parser)
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

            action.Parent = (IqlExpression)parser.Parse(action.Parent).Expression;

            if (literal != null &&
                (action.Kind == IqlExpressionKind.IsEqualTo && Equals(literal.Value, false)) ||
                (action.Kind == IqlExpressionKind.IsNotEqualTo && Equals(literal.Value, true)))
            {
                return parser.ReplaceAndParse(new IqlNotExpression(lr.SingleOrDefault(l => l != literal) ?? literal)).Expression;
            }

            action.Left = parser.Parse(action.Left).Expression;
            action.Right = parser.Parse(action.Right).Expression;

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
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlNavigationExpression action, IqlToIqlParserInstance parser)
        {
            if (action.Expands != null)
            {
                for (var i = 0; i < action.Expands.Count; i++)
                {
                    action.Expands[i] = (IqlExpandExpression)parser.Parse(action.Expands[i]).Expression;
                }
            }
            action.Filter = (IqlExpression)parser.Parse(action.Filter).Expression;
            action.WithKey = (IqlWithKeyExpression)parser.Parse(action.WithKey).Expression;
            action.Parent = (IqlExpression)parser.Parse(action.Parent).Expression;

            return action;
        }
    }


    public class IqlToIqlCollectitonQueryParser : IqlToIqlActionParserBase<IqlCollectitonQueryExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlCollectitonQueryExpression action, IqlToIqlParserInstance parser)
        {
            if (action.OrderBys != null)
            {
                for (var i = 0; i < action.OrderBys.Count; i++)
                {
                    action.OrderBys[i] = (IqlOrderByExpression)parser.Parse(action.OrderBys[i]).Expression;
                }
            }
            if (action.Expands != null)
            {
                for (var i = 0; i < action.Expands.Count; i++)
                {
                    action.Expands[i] = (IqlExpandExpression)parser.Parse(action.Expands[i]).Expression;
                }
            }
            action.Filter = (IqlExpression)parser.Parse(action.Filter).Expression;
            action.WithKey = (IqlWithKeyExpression)parser.Parse(action.WithKey).Expression;
            action.Parent = (IqlExpression)parser.Parse(action.Parent).Expression;

            return action;
        }
    }


    public class IqlToIqlDataSetQueryParser : IqlToIqlActionParserBase<IqlDataSetQueryExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlDataSetQueryExpression action, IqlToIqlParserInstance parser)
        {
            action.DataSet = (IqlDataSetReferenceExpression)parser.Parse(action.DataSet).Expression;
            var reference = (IqlDataSetReferenceExpression) action.DataSet;
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
                    action.OrderBys[i] = (IqlOrderByExpression)parser.Parse(action.OrderBys[i]).Expression;
                }
            }
            if (action.Expands != null)
            {
                for (var i = 0; i < action.Expands.Count; i++)
                {
                    action.Expands[i] = (IqlExpandExpression)parser.Parse(action.Expands[i]).Expression;
                }
            }
            action.Filter = (IqlExpression)parser.Parse(action.Filter).Expression;
            action.WithKey = (IqlWithKeyExpression)parser.Parse(action.WithKey).Expression;
            action.Parent = (IqlExpression)parser.Parse(action.Parent).Expression;

            return action;
        }
    }


    public class IqlToIqlExpandParser : IqlToIqlActionParserBase<IqlExpandExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlExpandExpression action, IqlToIqlParserInstance parser)
        {
            var property = parser.Parse(action.NavigationProperty).Expression;
            if (property is IqlPropertyExpression)
            {
                action.NavigationProperty = (IqlPropertyExpression)property;
            }
            if (action.Query != null)
            {
                var path = IqlPropertyPath.FromPropertyExpression(
                    parser.Adapter.EntityConfigurationContext.EntityType<TEntity>(),
                    action.NavigationProperty);
                action.Query = (IqlCollectitonQueryExpression)new IqlToIqlParserInstance(
                        parser.Adapter.EntityConfigurationContext.GetEntityByType(
                            path.Property.TypeDefinition.ElementType))
                    .Parse(action.Query).Expression;
            }

            if (property is IqlCountExpression)
            {
                action.Count = true;
            }
            action.Parent = (IqlExpression)parser.Parse(action.Parent).Expression;

            return action;
        }
    }


    public class IqlToIqlParser : IqlToIqlActionParserBase<IqlExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlExpression action, IqlToIqlParserInstance parser)
        {
            action.Parent = (IqlExpression)parser.Parse(action.Parent).Expression;

            return action;
        }
    }


    public class IqlToIqlNotParser : IqlToIqlActionParserBase<IqlNotExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlNotExpression action, IqlToIqlParserInstance parser)
        {
            action.Expression = (IqlExpression)parser.Parse(action.Expression).Expression;
            action.Parent = (IqlExpression)parser.Parse(action.Parent).Expression;

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
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlOrderByExpression action, IqlToIqlParserInstance parser)
        {
            action.OrderExpression = (IqlExpression)parser.Parse(action.OrderExpression).Expression;
            action.Parent = (IqlExpression)parser.Parse(action.Parent).Expression;

            return action;
        }
    }


    public class IqlToIqlParenthesisParser : IqlToIqlActionParserBase<IqlParenthesisExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlParenthesisExpression action, IqlToIqlParserInstance parser)
        {
            action.Expression = (IqlExpression)parser.Parse(action.Expression).Expression;
            action.Parent = (IqlExpression)parser.Parse(action.Parent).Expression;

            return action.Expression == null ? null : action;
        }
    }


    public class IqlToIqlParentValueParser : IqlToIqlActionParserBase<IqlParentValueExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlParentValueExpression action, IqlToIqlParserInstance parser)
        {
            action.Value = (IqlExpression)parser.Parse(action.Value).Expression;
            action.Parent = (IqlExpression)parser.Parse(action.Parent).Expression;

            if (action.Value == null && action.Kind == IqlExpressionKind.All)
            {
                return null;
            }

            return action;
        }
    }


    public class IqlToIqlStringSubStringParser : IqlToIqlActionParserBase<IqlStringSubStringExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlStringSubStringExpression action, IqlToIqlParserInstance parser)
        {
            action.Take = (IqlReferenceExpression)parser.Parse(action.Take).Expression;
            action.Value = (IqlExpression)parser.Parse(action.Value).Expression;
            action.Parent = (IqlExpression)parser.Parse(action.Parent).Expression;

            return action;
        }
    }


    public class IqlToIqlWithKeyParser : IqlToIqlActionParserBase<IqlWithKeyExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlWithKeyExpression action, IqlToIqlParserInstance parser)
        {
            if (action.KeyEqualToExpressions != null)
            {
                for (var i = 0; i < action.KeyEqualToExpressions.Count; i++)
                {
                    action.KeyEqualToExpressions[i] = (IqlIsEqualToExpression)parser.Parse(action.KeyEqualToExpressions[i]).Expression;
                }
            }
            action.Parent = (IqlExpression)parser.Parse(action.Parent).Expression;

            return action;
        }
    }


}