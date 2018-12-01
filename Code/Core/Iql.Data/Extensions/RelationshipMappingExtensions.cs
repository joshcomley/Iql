using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Context;
using Iql.Data.Queryable;
using Iql.Entities;
using Iql.Entities.Relationships;
using Iql.Entities.Rules.Relationship;
using Iql.Extensions;

namespace Iql.Data.Extensions
{
    public static class RelationshipMappingExtensions
    {
        static RelationshipMappingExtensions()
        {
            GetRelationshipFilterRuleMethod = typeof(RelationshipMappingExtensions).GetMethod(
                nameof(GetRelationshipFilterRuleAsync),
                BindingFlags.NonPublic | BindingFlags.Static);
        }

        private static MethodInfo GetRelationshipFilterRuleMethod { get; }

        public static Task<IRelationshipRule> GetRelationshipRuleAsync(
            this RelationshipMapping mapping,
            IProperty relatedProperty,
            object entity,
            IDataContext dataContext)
        {
            return (Task<IRelationshipRule>)GetRelationshipFilterRuleMethod.InvokeGeneric(
                null,
                new object[] { mapping, relatedProperty, entity, dataContext },
                entity.GetType(),
                mapping.Container.OtherSide.Type,
                mapping.Container.EntityConfiguration.Type);
        }
        private static async Task<IRelationshipRule>
            GetRelationshipFilterRuleAsync<TEntity, TProperty, TRelationship>(
                RelationshipMapping mapping,
                IProperty relatedProperty,
                TEntity entity,
                IDataContext dataContext)
        {
            var existingLambda = mapping.Expression as IqlLambdaExpression;
            var innerLambda = existingLambda.Body as IqlLambdaExpression;
            var propertyExpression = innerLambda.Body.Clone() as IqlPropertyExpression;
            var current = propertyExpression as IqlExpression;
            var lastParent = current;
            IqlExpression rootRefBackup = null;
            Type pathType = null;
            while (true)
            {
                if (current.Parent == null)
                {
                    break;
                }

                if (current.Parent.Kind == IqlExpressionKind.Property &&
                    current.Parent.Parent != null &&
                    (current.Parent.Parent.Kind == IqlExpressionKind.RootReference ||
                     current.Parent.Parent.Kind == IqlExpressionKind.Variable))
                {
                    rootRefBackup = current.Parent;
                    current.Parent = new IqlRootReferenceExpression();
                    pathType = typeof(TEntity);
                    break;
                }

                if (current.Parent.Kind == IqlExpressionKind.RootReference &&
                    (current.Parent as IqlRootReferenceExpression).EntityTypeName == typeof(TProperty).Name)
                {
                    pathType = typeof(TProperty);
                    break;
                }
                current = current.Parent;
            }

            var path = IqlPropertyPath.FromPropertyExpression(
                dataContext.EntityConfigurationContext.GetEntityByType(pathType),
                propertyExpression);
            var equalityExpessions = new List<IqlExpression>();
            for (var i = 0; i < mapping.Container.Constraints.Length; i++)
            {
                var thisEndConstraint = mapping.Container.Constraints[i];
                var otherEndConstraint = path.Property.Relationship.ThisEnd.Constraints[i];
                var p = propertyExpression.Clone() as IqlPropertyExpression;
                p.Parent = rootRefBackup ?? p.Parent;
                p.PropertyName = otherEndConstraint.PropertyName;
                equalityExpessions.Add(GetEqualityExpression(
                    mapping.Container.EntityConfiguration.Name,
                    thisEndConstraint.PropertyName,
                    p));
            }
            var iql = CreateRelationshipFilterIql(
                relatedProperty.EntityConfiguration.Name,
                mapping.Container.EntityConfiguration.Name,
                equalityExpessions.And()
            );
            //iql = CreateRelationshipFilterIql();
            iql.ReplaceWith((context, expression) =>
            {
                if (expression is IqlVariableExpression &&
                    (expression as IqlVariableExpression).EntityTypeName == iql.Parameters[0].EntityTypeName)
                {
                    return iql.Parameters[0];
                }
                if (expression is IqlVariableExpression &&
                    (expression as IqlVariableExpression).EntityTypeName == (iql.Body as IqlLambdaExpression).Parameters[0].EntityTypeName)
                {
                    return (iql.Body as IqlLambdaExpression).Parameters[0];
                }
                return expression;
            });
            var expression1 = IqlConverter.Instance.ConvertIqlToExpression<RelationshipFilterContext<TEntity>>(iql);
            var rule = new RelationshipFilterRule<TEntity, TRelationship>((Expression<Func<RelationshipFilterContext<TEntity>, Expression<Func<TRelationship, bool>>>>)expression1, null, null);
            return rule;
        }

        private static IqlLambdaExpression CreateRelationshipFilterIql(
            string ownerTypeName,
            string childTypeName,
            IqlExpression body)
        {
            return new IqlLambdaExpression
            {
                Body = new IqlLambdaExpression
                {
                    Body = body,
                    Parameters = new List<IqlRootReferenceExpression>
                    {
                        new IqlRootReferenceExpression
                        {
                            EntityTypeName = childTypeName,
                            VariableName = "child",
                            InferredReturnType = IqlType.Unknown,
                            Kind = IqlExpressionKind.RootReference,
                            ReturnType = IqlType.Unknown
                        }
                    },
                    Kind = IqlExpressionKind.Lambda,
                    ReturnType = IqlType.Unknown
                },
                Parameters = new List<IqlRootReferenceExpression>
                {
                    new IqlRootReferenceExpression
                    {
                        EntityTypeName = $"{nameof(RelationshipFilterContext<object>)}<{ownerTypeName}>",
                        VariableName = "context",
                        InferredReturnType = IqlType.Unknown,
                        Kind = IqlExpressionKind.RootReference,
                        ReturnType = IqlType.Unknown
                    }
                },
                Kind = IqlExpressionKind.Lambda,
                ReturnType = IqlType.Unknown
            };
        }

        private static IqlIsEqualToExpression GetEqualityExpression(
            string childTypeName, 
            string childPropertyName, 
            IqlExpression propertyIql)
        {
            return new IqlIsEqualToExpression
            {
                Left = GetChildPropertyExpression(childTypeName, childPropertyName),
                Right = propertyIql,
                Kind = IqlExpressionKind.IsEqualTo,
                ReturnType = IqlType.Unknown
            };
        }

        private static IqlPropertyExpression GetChildPropertyExpression(string childTypeName, string childPropertyName)
        {
            return new IqlPropertyExpression
            {
                PropertyName = childPropertyName,
                Kind = IqlExpressionKind.Property,
                ReturnType = IqlType.Unknown,
                Parent = new IqlRootReferenceExpression
                {
                    EntityTypeName = childTypeName,
                    VariableName = "child",
                    InferredReturnType = IqlType.Unknown,
                    Kind = IqlExpressionKind.RootReference,
                    ReturnType = IqlType.Unknown
                }
            };
        }
    }
}